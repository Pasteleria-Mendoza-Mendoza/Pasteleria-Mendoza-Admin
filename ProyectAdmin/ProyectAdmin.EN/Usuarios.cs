using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAdmin.EN
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        [ForeignKey("Rol")]
        public int RolId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public byte Estado { get; set; }
        public Rol Rol { get; set; }
    }

    public enum EstadoUsuario
    {
        Activo = 1,
        Inactivo = 2
    }
}
