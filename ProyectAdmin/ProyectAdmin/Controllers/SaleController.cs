using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs.SaleDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    public class SaleController : Controller
    {
        readonly ISaleBL _saleBL;

        public SaleController(ISaleBL saleBL)
        {
            _saleBL = saleBL;
        }

        // GET: SaleController
        public async Task<IActionResult> Index(SaleSearchInputDTO inputDTO)
        {
            var list = await _saleBL.Search(inputDTO);
            return View(list);
        }

        // GET: SaleController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            SaleGetByIdDTO xproduct = await _saleBL.GetById(id);
            var result = new SaleUpdateDTO()
            {
                Id = xproduct.Id,
                TypeCake = xproduct.TypeCake,
                CakeDimensions = xproduct.CakeDimensions,
                ReservationDate = xproduct.ReservationDate,
                DeliverDate = xproduct.DeliverDate,
                Cost = xproduct.Cost
            };
            return View(xproduct);
        }

        // GET: SaleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleController/Create
        [HttpPost]
        public async  Task<IActionResult> Create(SaleAddDTO saleAddDTO)
        {
            try
            {
                int Result = await _saleBL.Create(saleAddDTO);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(saleAddDTO);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: SaleController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            SaleGetByIdDTO sale = await _saleBL.GetById(id);
            var sales = new SaleUpdateDTO()
            {
                Id = sale.Id,
                TypeCake = sale.TypeCake,
                CakeDimensions = sale.CakeDimensions,
                ReservationDate = sale.ReservationDate,
                DeliverDate = sale.DeliverDate,
                Cost = sale.Cost

            };
            return View();
        }

        // POST: SaleController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SaleUpdateDTO updateDTO)
        {
            try
            {
                int result = await _saleBL.Update(updateDTO);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(updateDTO);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: SaleController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            SaleGetByIdDTO sale = await _saleBL.GetById(id);
            return View(sale);
        }

        // POST: SaleController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id, SaleGetByIdDTO sale)
        {
            try
            {
                int result = await _saleBL.Delete(id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(sale);
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
