using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.Models;
using ProyectAdmin.Data;

namespace ProyectAdmin.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Admin _admin)
        {
            DA_logica _da_admin = new DA_logica();

            var admin = _da_admin.ValidarAdmin(_admin.Email, _admin.Password);

            if (admin != null)
            {
                return RedirectToAction("Index, Home");
            }
            else
            {
                return View();
            }

        }
    }
}
