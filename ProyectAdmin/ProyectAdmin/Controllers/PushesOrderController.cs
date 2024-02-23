using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs.PushesOrderDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    public class PushesOrderController : Controller
    {
        readonly IPushesOrderBL _pushesOrderBL;

        public PushesOrderController(IPushesOrderBL pushesOrderBL)
        {
            _pushesOrderBL = pushesOrderBL;
        } 

        // GET: PushesOrderController
        public async Task<IActionResult> Index(PushesOrderSearchInputDTO inputDTO)
        {
            var list = await _pushesOrderBL.Search(inputDTO);
            return View(list);
        }

        // GET: PushesOrderController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PushesOrderGetByIdDTO xpushesOrders = await _pushesOrderBL.GetById(id);
            var Result = new PushesOrderUpdateDTO()
            {
                Id = xpushesOrders.Id
            };
            return View(xpushesOrders);
        }

        // GET: PushesOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PushesOrderController/Create
        [HttpPost]
        public async Task<IActionResult> Create(PushesOrderAddDTO pushesOrderAddDTO)
        {
            try
            {
                // Crear un objeto PushesOrderCreateInputDTO utilizando datos de pushesOrderAddDTO u otra fuente
                var createInputDTO = new PushesOrderCreateInputDTO
                {
                    CustomerName = pushesOrderAddDTO.CustomerName,
                    ContactNumber = pushesOrderAddDTO.ContactNumber,
                    // Otros campos según sea necesario
                };

                // Llamar al método Create en _pushesOrderBL y obtener el resultado
                PushesOrderCreateOutputDTO result = await _pushesOrderBL.Create(createInputDTO);

                // Verificar si el resultado es exitoso
                if (result != null && result.Id > 0) // O cualquier condición que indique éxito
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pushesOrderAddDTO);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }



        // GET: PushesOrderController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            PushesOrderGetByIdDTO pushesOrder = await _pushesOrderBL.GetById(id);
            var pushesOrders = new PushesOrderUpdateDTO()
            {
                Id = pushesOrder.Id,
                CustomerName = pushesOrder.CustomerName,
                ContactNumber = pushesOrder.ContactNumber,
                CakeQuantity = pushesOrder.CakeQuantity,
                CakeDimensions = pushesOrder.CakeDimensions,
                CakeDedication = pushesOrder.CakeDedication,
                ReservationDate = pushesOrder.ReservationDate,
                CakeCost = pushesOrder.CakeCost,

            };
            return View(pushesOrders);
        }

        // POST: PushesOrderController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PushesOrderUpdateDTO updateDTO)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PushesOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PushesOrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
