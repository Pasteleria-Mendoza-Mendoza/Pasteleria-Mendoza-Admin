using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectAdmin.BL.DTOs.EmailDTO;
using ProyectAdmin.BL.DTOs.RolDTOs;
using ProyectAdmin.BL.DTOs.UsuariosDTOs;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        readonly IUsuarioBL _usuarioBL;
        readonly IRolBL _rolBL;
       readonly IEmailBL _email;

        public UsuariosController(IUsuarioBL usuarioBL, IRolBL rolBL, IEmailBL email)
        {
            _usuarioBL = usuarioBL;
            _rolBL = rolBL;
            _email = email;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            if (TempData.ContainsKey("DangerMessage"))
            {
                ViewBag.DangerMessage = TempData["DangerMessage"];
            }

            var usuarios = await _usuarioBL.Search(new UsuarioSearchInputDTO());
            return View(usuarios);
        }

        public async Task<ActionResult> Create()
        {

            // Crear una instancia de RolSearchingInputDTO
            RolSearchingInputDTO rol = new RolSearchingInputDTO();

            // Buscar los roles y asignarlos a una variable
            var rolList = await _rolBL.Search(rol);

            ViewBag.Roles = rolList;

            // Retornar la vista
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(CreateUsuarioDTO pUsuario, string Usuario)
        {
            try
            {
                int result = await _usuarioBL.Create(pUsuario);
                if (result > 0)
                {
                    #region Envio de correo
                    // Crear un objeto EmailDTO con la información necesaria para enviar el correo electrónico
                    var emailDTO = new EmailDTO
                    {
                        Para = pUsuario.Correo,
                        Asunto = "Bienvenido a nuestra aplicación",
                        Contenido = $@"
                <html>
                <head>
                    <link rel='stylesheet' type='text/css' href='~/css/email.css'>
                </head>
                <body>
                    <div class='container'>
                        <h1>Bienvenido a MAURITA</h1>
                        <p>Hola {pUsuario.Nombre},</p>
                        <p>¡Bienvenido a nuestra aplicación! </p>
                        <p>Tu nombre de usuario es: {pUsuario.Usuario}</p>
                        <p>Tu contraseña es: {pUsuario.Contraseña}</p>
                        <p>puedes ingresar al sistema!.</p>
                    </div>
                </body>
                </html>"
                    };

                    await _email.EnviarEmail(emailDTO);

                    #endregion

                    TempData["SuccessMessage"] = $"Usuario {Usuario} creado exitosamente!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pUsuario);
                }
            }
            catch (Exception)
            {
                RolSearchingInputDTO rol = new RolSearchingInputDTO();
                var roles = await _rolBL.Search(rol);
                ViewBag.Roles = roles;

                return View();
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var usuario = await _usuarioBL.GetById(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                // Obtén la lista de roles y pásalos a la vista
                RolSearchingInputDTO rol = new RolSearchingInputDTO();
                var roles = await _rolBL.Search(rol);
                ViewBag.Roles = roles;

                // Make sure to pass UsuarioUpdateDTO to the view
                var usuarioUpdateDTO = new UsuarioUpdateDTO
                {
                    UsuarioId = usuario.UsuarioId,
                    RolId = usuario.RolId,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.Correo,
                    Usuario = usuario.Usuario,
                    Estado = (BL.DTOs.UsuariosDTOs.EstadoUsuario)usuario.Estado
                };

                return View(usuarioUpdateDTO);
            }
            catch (Exception)
            {
                // Manejo de error
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsuarioUpdateDTO usuario)
        {
            try
            {
                if (id != usuario.UsuarioId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _usuarioBL.Update(usuario);
                    return RedirectToAction(nameof(Index));
                }

                // Si la validación del modelo falla, vuelve a obtener la lista de roles y pásalos a la vista
                RolSearchingInputDTO rol = new RolSearchingInputDTO();
                var roles = await _rolBL.Search(rol);
                ViewBag.Roles = roles;

                return View(usuario);
            }
            catch (Exception)
            {
                // Manejo de error
                return View("Error");
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioBL.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuarioBL.Delete(id);
            TempData["DangerMessage"] = $"Usuario eliminado exitosamente!";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var usuario = await _usuarioBL.GetById(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                // Puedes crear una vista Details.cshtml en tu carpeta Views/Usuario con el formato que desees
                return View(usuario);
            }
            catch (Exception)
            {
                // Manejo de error
                return View("Error");
            }
        }
    }
}
