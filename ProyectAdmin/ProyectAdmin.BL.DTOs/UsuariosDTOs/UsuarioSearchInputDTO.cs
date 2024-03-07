using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.UsuariosDTOs
{
    public class UsuarioSearchInputDTO
    {
        public string? NombreLike { get; set; }
        public string? TelefonoLike { get; set; }
    }
}
