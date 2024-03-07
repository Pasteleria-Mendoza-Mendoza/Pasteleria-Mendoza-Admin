using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.UsuariosDTOs
{
    public class SecurityDTO
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Login { get; set; }
        public int RolId { get; set; }
        public string NombreRol { get; set; }

    }
}
