using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs.ProductDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    public class Products : Controller
    {
        readonly IProductBL _ProductBL;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Products(IProductBL productBL, IWebHostEnvironment webHostEnvironment)
        {
            _ProductBL = productBL;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(ProductSearchOutputDTO productos)
        {
            // Verificar si el usuario está autenticado y tiene el rol de "Administrador"
            bool isAdmin = User.Identity.IsAuthenticated && User.IsInRole("Administrador");

            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            if (TempData.ContainsKey("WarningMessage"))
            {
                ViewBag.WarningMessage = TempData["WarningMessage"];
            }
            var list = await _ProductBL.Search(productos);

            // Establecer la variable ViewBag.IsAdmin para usarla en la vista
            ViewBag.IsAdmin = isAdmin;

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {

            ProductGetByIdDTO productId = new ProductGetByIdDTO { IdProduct = id };
            ProductGellAllDTO producto = await _ProductBL.SearchOne(productId);
            return View(producto);
        }
    }
}
