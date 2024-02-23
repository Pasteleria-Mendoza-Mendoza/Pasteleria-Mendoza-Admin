using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.BL.DTOs.ProductDTOs;

namespace ProyectAdmin.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductBL _ProductBL;

        public ProductController(IProductBL productBL)
        {
            _ProductBL = productBL;
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
            try
            {
                List<ProductSearchOutputDTO> list = await _ProductBL.Search(productos);
         
                return View(list);
            }
            catch (Exception)
            {
                // Manejo de error
                return View("Error");
            }
        }



        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int id)
        {

            ProductGetByIdDTO productId = new ProductGetByIdDTO { IdProduct = id };
            ProductGellAllDTO producto = await _ProductBL.SearchOne(productId);
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateInputDTO pProductos, string Nombre)
        {
            try
            {

                // Intenta crear el producto
                ProductCreateOutputDTO result = await _ProductBL.CreateProduct(pProductos);

                // Si se crea el producto correctamente, redirige a la acción Index con un mensaje de éxito
                TempData["SuccessMessage"] = $"Producto {pProductos.NameProduct} creado exitosamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                // Si ya existe un producto con el mismo nombre, muestra un mensaje de error en la vista
                ViewBag.ErrorMessage = ex.Message;
                return View(pProductos);
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos o registro de errores
                ViewBag.ErrorMessage = $"Error al intentar crear el producto: {ex.Message}";
                return View(pProductos);
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
