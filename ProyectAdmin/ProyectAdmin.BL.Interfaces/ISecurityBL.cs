using ProyectAdmin.BL.DTOs.UsuariosDTOs;
using ProyectAdmin.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface ISecurityBL
    {
        public Usuarios ChangePassword(Usuarios usuarios, string ContraseñaAnt);
        public SecurityDTO Login(string Login, string Password);
    }
}
