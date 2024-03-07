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
            UsuarioId = uUsuario.UsuarioId;
            RolId = uUsuario.RolId;
            Nombre = uUsuario.Nombre;
            Apellido = uUsuario.Apellido;
            Correo = uUsuario.Correo;
            Usuario = uUsuario.Usuario;
            Estado = (EstadoUsuario)uUsuario.Estado;
        }

        public int UsuarioId { get; set; }

        [ForeignKey("Rol")]
        [Required(ErrorMessage = "Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Apellido { get; set; }

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
