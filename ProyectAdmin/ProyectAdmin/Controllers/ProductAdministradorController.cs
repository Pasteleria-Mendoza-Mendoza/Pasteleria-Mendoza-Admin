using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs;
using ProyectAdmin.BL.DTOs.OrdersDTOs;
using ProyectAdmin.BL.DTOs.ProductDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class ProductAdministradorController : Controller
    {
        readonly IProductBL _ProductBL;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductAdministradorController(IProductBL productBL, IWebHostEnvironment webHostEnvironment)
        {
            _ProductBL = productBL;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(ProductSearchOutputDTO productos)
        {
              if (TempData.ContainsKey("SuccessMessage"))
             {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            if (TempData.ContainsKey("WarningMessage"))
            {
               ViewBag.WarningMessage = TempData["WarningMessage"];
            }
            var list = await _ProductBL.Search(productos);
            return View(list);
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
        public async Task<ActionResult> Create(ProductCreateInputDTO producto, IFormFile Imagen)
        {
            try
            {
                Stream image = Imagen.OpenReadStream();
                // Llamamos a nuestra interfaz para subir el archivo
                string urlimagen = await _ProductBL.SubirArchivo(image, Imagen.FileName);

                // Creamos un objeto ProductCreateInputDTO a partir de los datos del objeto Product
                ProductCreateInputDTO productDTO = new ProductCreateInputDTO()
                {
                    NameProduct = producto.NameProduct,
                    Dimensions = producto.Dimensions,
                    Description = producto.Description,
                    Quantity = producto.Quantity,
                    Price = producto.Price,
                    AcquisitionDate = producto.AcquisitionDate,
                    DueDate = producto.DueDate,
                    ImageUrl = urlimagen // Asignamos la URL de la imagen recién subida
                };

                // Llamamos al método CreateProduct con el objeto ProductCreateInputDTO
                ProductCreateOutputDTO result = await _ProductBL.CreateProduct(productDTO);

                if (result != null)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "No se pudo agregar el registro";
                    return View(producto);
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
