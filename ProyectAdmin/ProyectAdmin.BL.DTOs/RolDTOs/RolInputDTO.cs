using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.RolDTOs
{
    public class RolInputDTO
    {
        public int RolId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        public byte Estado { get; set; }
    }
}
