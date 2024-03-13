using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs.EmailDTO;
using ProyectAdmin.BL.DTOs.OrdersDTOs;
using ProyectAdmin.BL.DTOs.ProductDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    public class OrderController : Controller
    {
        readonly IPushesOrderBL _pushesOrderBL;
        readonly IProductBL _productBL;
        readonly IEmailBL _email;
        public OrderController(IPushesOrderBL pushesOrderBL, IProductBL productBL, IEmailBL email)
        {
            _pushesOrderBL = pushesOrderBL;
            _productBL = productBL;
            _email = email;
        }

        // Método para mostrar el formulario vacío de creación de órdenes
        public async Task<ActionResult> Create()
        {
            try
            {
                // Obtener la lista de todos los productos desde _productosBL
                var productosList = await _productBL.Search(new ProductSearchOutputDTO());
                ViewBag.Productos = productosList;

                return View(new CreateOrderInputDTO());
            }
            catch (Exception ex)
            {
                // Manejo de error
                ViewBag.ErrorMessage = "Error al obtener la lista de productos";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderInputDTO orderInputDTO)
        {
            try
            {
                // Llamar al método para agregar la orden usando el servicio BL correspondiente
                var createdOrder = await _pushesOrderBL.AddOrder(orderInputDTO);

                #region Envio de correo
                // Crear un objeto EmailDTO con la información necesaria para enviar el correo electrónico
                var emailDTO = new EmailDTO
                {
                    Para = orderInputDTO.Correo,
                    Asunto = "Reservación",
                    Contenido = $@"
                    <html>
                        <body>
                            <div class='container'>
                                <h1>Pasteleria MENDOZA</h1>
                                <p>Hola {orderInputDTO.Names},</p>
                                <p>¡Gracias por tu orden de pastel!</p>
                                <p>Tu pedido ha sido confirmado y será procesado pronto.</p>
                                <p>¡Esperamos que disfrutes de nuestros deliciosos pasteles!</p>
                            </div>
                        </body>
                    </html>"
                };

                // Configurar el mensaje de éxito
                TempData["SuccessMessage"] = "Se añadió el producto a la orden.";

                // Llamar al método EnviarEmail de EmailBL para enviar el correo electrónico
                // Llamar al método EnviarEmail de EmailBL para enviar el correo electrónico
                await _email.EnviarEmail(emailDTO);


                #endregion

                // Permanecer en la vista Index de Productos
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                TempData["ErrorMessage"] = $"Error al crear la orden: {ex.Message}";
                return RedirectToAction("Index", "Products");
            }
        }
    }
}
