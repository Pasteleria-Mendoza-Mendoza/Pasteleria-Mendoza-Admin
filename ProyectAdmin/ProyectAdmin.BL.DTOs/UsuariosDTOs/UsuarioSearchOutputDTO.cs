using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.UsuariosDTOs
{
    public class UsuarioSearchOutputDTO
    {
        [DisplayName("Id Usuario")]
        public int UsuarioId { get; set; }
        [DisplayName("Rol")]
        public int RolId { get; set; }

        [DisplayName("NombreRol")]
        public string NombreRol { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        [DisplayName("Apellido")]
        public string Apellido { get; set; }

        [DisplayName("Usuario")]
        public string? Usuario { get; set; }
        [DisplayName("Correo")]
        public string? Correo { get; set; }
        [DisplayName("Estado")]
        public byte Estado { get; set; }

    }
}
