using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.UsuariosDTOs
{
    public class UsuarioUpdateDTO
    {
        public UsuarioUpdateDTO()
        {

        }
        public UsuarioUpdateDTO(UsuarioSearchOutputDTO uUsuario)
        {
            SucursalesId = uUsuario.SucursalesId;
            UsuarioId = uUsuario.UsuarioId;
            RolId = uUsuario.RolId;
            Nombre = uUsuario.Nombre;
            Apellido = uUsuario.Apellido;
            Telefono = uUsuario.Telefono;
            Dui = uUsuario.Dui;
            Correo = uUsuario.Correo;
            Usuario = uUsuario.Usuario;
            Estado = (EstadoUsuario)uUsuario.Estado;
        }

        public int UsuarioId { get; set; }

        [ForeignKey("Rol")]
        [Required(ErrorMessage = "Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        [ForeignKey("Sucursales")]
        [Required(ErrorMessage = "Sucursal es obligatorio.")]
        [Display(Name = "Sucursal")]
        public int SucursalesId { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Teléfono es obligatorio.")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El DUI es obligatorio.")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Dui { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El usuario para iniciar sesión es obligatorio.")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El Estado es requerido.")]
        public EstadoUsuario Estado { get; set; }

    }

    public enum EstadoUsuario
    {
        Activo = 1,
        Inactivo = 2
    }
}
