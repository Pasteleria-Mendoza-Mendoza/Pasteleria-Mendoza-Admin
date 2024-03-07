using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.UsuariosDTOs
{
    public class CreateUsuarioDTO
    {
        [Required(ErrorMessage = "Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "Sucursal es obligatorio.")]
        [Display(Name = "Sucursal")]
        public int SucursalesId { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Teléfono es obligatorio.")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Dui es obligatorio.")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        public string Dui { get; set; }

        [Required(ErrorMessage = "El Correo es obligatorio")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El usuario para iniciar sesion es obligatorio.")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatorio. ")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Contraseña debe estar entre 5 y 32 caracteres", MinimumLength = 5)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "El Estado es requerido, 1 activo, 2 inactivo.")]
        public byte Estado { get; set; }

    }
}
