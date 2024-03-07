using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectAdmin.BL.DTOs.RolDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class RolController : Controller
    {
        private readonly IRolBL _rolBL;

        public RolController(IRolBL rolBL)
        {
            _rolBL = rolBL;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _rolBL.Search(new RolSearchingInputDTO());
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolInputDTO rol)
        {
            if (ModelState.IsValid)
            {
                await _rolBL.Create(rol);
                return RedirectToAction(nameof(Index));
            }

            return View(rol);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rol = await _rolBL.GetById(id);
            if (rol == null)
            {
                return NotFound();
            }

            var rolInputModel = new RolInputDTO
            {
                RolId = rol.RolId,
                Nombre = rol.Nombre,
                // Otros campos según sea necesario
            };

            return View(rolInputModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, RolInputDTO rol)
        {
            if (id != rol.RolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _rolBL.Update(rol);
                }
                catch
                {
                    // Manejar excepciones según sea necesario
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(rol);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _rolBL.GetById(id);
            if (rol == null)
            {
                return NotFound();
            }

            var rolInputModel = new RolInputDTO
            {
                RolId = rol.RolId,
                Nombre = rol.Nombre,
                // Otros campos según sea necesario
            };

            return View(rolInputModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _rolBL.Delete(id);
                // Elimina el TempData para asegurarse de que no se muestre en el próximo acceso al Index
                TempData.Remove("ErrorMessage");
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    // Número 547 es específico para una violación de restricción de clave externa (FOREIGN KEY)
                    // Agrega un mensaje de advertencia en TempData
                    TempData["ErrorMessage"] = "Este rol no puede ser eliminado porque está asociado a un usuario.";
                }
                else
                {
                    // Si la excepción no es de clave externa, re-lanza la excepción
                    throw;
                }

                // Redirige a la misma acción (Delete) para mostrar la vista con el mensaje de advertencia
                return RedirectToAction(nameof(Delete), new { id });
            }
        }

    }
}
