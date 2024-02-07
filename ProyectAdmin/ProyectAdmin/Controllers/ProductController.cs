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

        public async Task<IActionResult> Index(ProductSearchInputDTO xProduct)
        {
            var list = await _ProductBL.Search(xProduct);
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            ProductGetByIdDTO xProduct = await _ProductBL.GetById(id);
            var result = new ProductUpdateDTO()
            {
                Name = xProduct.Name,
                Quantity = xProduct.Quantity,
                Dimensions = xProduct.Dimensions,
                AcquisitionDate = xProduct.AcquisitionDate,
                DueDate = xProduct.DueDate,
            };
            return View(xProduct);
        }

        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAddDTO xProduct)
        {
            try
            {
                int result = await _ProductBL.Create(xProduct);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(xProduct);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProductGetByIdDTO xProducto = await _ProductBL.GetById(id);
            var productResult = new ProductUpdateDTO()
            {
                Id = xProducto.Id,
                Name = xProducto.Name,
                Quantity = xProducto.Quantity,
                Dimensions = xProducto.Dimensions,
                AcquisitionDate = xProducto.AcquisitionDate,
                DueDate = xProducto.DueDate,
            };
            return View(productResult);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductUpdateDTO xProduct)
        {
            try
            {
                int result = await _ProductBL.Update(xProduct);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(xProduct);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            ProductGetByIdDTO product = await _ProductBL.GetById(id);
            return View(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, ProductGetByIdDTO xProduct)
        {
            try
            {
                int result = await _ProductBL.Delete(id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(xProduct);
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
