using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs.OrdersDTOs;
using ProyectAdmin.BL.DTOs.ProductDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN;
using static ProyectAdmin.EN.PushesOrder;

namespace ProyectAdmin.Controllers
{
    public class PushesOrderController : Controller
    {
        readonly IPushesOrderBL _pushesOrderBL;
        readonly IProductBL _productBL;

        public PushesOrderController(IPushesOrderBL pushesOrderBL, IProductBL productBL)
        {
            _pushesOrderBL = pushesOrderBL;
            _productBL = productBL;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(CreateOrderInputDTO orden)
        {
            // Llamar al método para agregar la orden usando el servicio BL correspondiente
            var createdOrder = await _pushesOrderBL.AddOrder(orden);

            // Configurar el mensaje de éxito
            TempData["SuccessMessage"] = "Se añadió el producto a la orden.";

            // Permanecer en la vista Index de Productos
            return RedirectToAction("Index", "Product");
        }


        public async Task<IActionResult> Index(DateTime? specificDate = null)
        {
            // Obtener la lista de todas los productos para mapear los nombres en lugar de los IDs
            var productosList = await _productBL.Search(new ProductSearchOutputDTO());
            var productosDictionary = productosList.ToDictionary(c => c.IdProduct, c => c.NameProduct);

            // Obtener la lista de órdenes para la fecha específica
            List<GetAllOrderOutputDTO> list;
            if (!specificDate.HasValue)
            {
                specificDate = DateTime.Today; // Obtener la fecha actual si no se especifica ninguna fecha
            }
            list = await _pushesOrderBL.GetAllOrder(specificDate);

            // Mapear los IDs de productos a los nombres en la lista de órdenes
            foreach (var orden in list)
            {
                if (productosDictionary.TryGetValue(orden.IdProduct, out var nombreProducto))
                {
                    orden.nombreProducto = nombreProducto;
                }
            }


            // Configurar ViewBag.Sucursales con la lista de productos
            ViewBag.Productos = productosList;

            return View(list);
        }
      
        public async Task<ActionResult> OrdenDetails(int Id)
        {
            GetAllOrderOutputDTO orden = await _pushesOrderBL.GetOrderById(Id);

            return View(orden);

        }

        [HttpPost]
        public async Task<ActionResult> AutorizarOrden(int id)
        {
            try
            {
                // Lógica para autorizar la orden con el ID proporcionado
                await _pushesOrderBL.AutorizarPedidoAsync(id);

                TempData["SuccessMessage"] = "La orden ha sido autorizada correctamente.";
            }
            catch (Exception ex)
            {
                // Manejo de errores
                TempData["ErrorMessage"] = $"Error al autorizar la orden: {ex.Message}";
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<ActionResult> RechazarOrden(int id)
        {
            try
            {
                // Llamar al método de la capa de negocios para rechazar la orden
                await _pushesOrderBL.RechazarPedidoAsync(id);

                TempData["SuccessMessage"] = "La orden ha sido rechazada correctamente.";
            }
            catch (Exception ex)
            {
                // Manejo de errores
                TempData["ErrorMessage"] = $"Error al rechazar la orden: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _pushesOrderBL.DeleteOrden(id);
                TempData["SuccessMessage"] = "La orden se eliminó correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar la compra con ID {id}.";
                return RedirectToAction("Error");
            }
        }

    }
}

