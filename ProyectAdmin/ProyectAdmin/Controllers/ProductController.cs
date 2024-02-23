using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.BL.DTOs.ProductDTOs;
using Inventory.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using ProyectAdmin.BL;

namespace ProyectAdmin.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductBL _ProductBL;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductBL productBL, IWebHostEnvironment webHostEnvironment)
        {
            _ProductBL = productBL;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(ProductSearchOutputDTO productos)
        {
          //  if (TempData.ContainsKey("SuccessMessage"))
            //{
              //  ViewBag.SuccessMessage = TempData["SuccessMessage"];
            //}
            //if (TempData.ContainsKey("WarningMessage"))
            //{
              //  ViewBag.WarningMessage = TempData["WarningMessage"];
            //}
            var list = await _ProductBL.Search(productos);
            return View(list);
        }



        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int id)
        {

            ProductGetByIdDTO productId = new ProductGetByIdDTO { IdProduct = id };
            ProductGellAllDTO producto = await _ProductBL.SearchOne(productId);
            return View(producto);
        }

        public async Task<IActionResult> DetailsPartial(int id)
        {

            ProductGetByIdDTO productId = new ProductGetByIdDTO { IdProduct = id };
            ProductGellAllDTO producto = await _ProductBL.SearchOne(productId);
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMenssge = "";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ImageInputDTO pProducts)
        {
            try
            {
                string fileName = null;

                ProductCreateInputDTO product = new ProductCreateInputDTO()
                {
                    NameProduct = pProducts.NameProduct,
                    Dimensions = pProducts.Dimensions,
                    Description = pProducts.Description,
                    Quantity = pProducts.Quantity,
                    Price = pProducts.Price,
                    AcquisitionDate = pProducts.AcquisitionDate,
                    DueDate = pProducts.DueDate
                };

                if (pProducts.ImageUrl != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(pProducts.ImageUrl.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await pProducts.ImageUrl.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = fileName;
                }

                ProductCreateOutputDTO result = await _ProductBL.CreateProduct(product);
                if (result != null)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "No se pudo agregar el registro";
                    return View(pProducts);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Se produjo un error al procesar la solicitud: " + ex.Message;
                if (ex.InnerException != null)
                {
                    ViewBag.InnerErrorMessage = "Detalles del error interno: " + ex.InnerException.Message;
                }
                return View(new ProductCreateInputDTO());
            }


        }



        // Resto de las acciones (Edit y Delete)...

        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ProductGetByIdDTO productId = new ProductGetByIdDTO { IdProduct = id };
            ProductGellAllDTO producto = await _ProductBL.SearchOne(productId);

            ProductUpdateDTO product = new ProductUpdateDTO
            {
                IdProduct = producto.IdProduct,
                NameProduct = producto.NameProduct,
                Quantity = producto.Quantity,
                Dimensions = producto.Dimensions,
                AcquisitionDate = producto.AcquisitionDate,
                DueDate = producto.DueDate
            };

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProductUpdateDTO product)
        {
            try
            {
                await _ProductBL.UpdateProduct(product);

                TempData["SuccessMessage"] = "Producto actualizado exitosamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error en la actualización: " + ex.Message;
                return View(product);
            }
        }


        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ProductGetByIdDTO productId = new ProductGetByIdDTO { IdProduct = id };
            ProductGellAllDTO producto = await _ProductBL.SearchOne(productId);
            ProductDeleteDTO deleteProduct = new ProductDeleteDTO { IdProduct = producto.IdProduct };

            return View(deleteProduct);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, ProductDeleteDTO deleteProduct)
        {
            try
            {
                ProductDeleteDTO result = await _ProductBL.DeleteProduct(deleteProduct);

                if (result.IsDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "No se pudo eliminar el producto";
                    return View(deleteProduct);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
