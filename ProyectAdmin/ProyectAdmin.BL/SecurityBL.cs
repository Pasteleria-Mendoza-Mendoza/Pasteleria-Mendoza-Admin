using ProyectAdmin.BL.DTOs.UsuariosDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN.Interfaces;
using ProyectAdmin.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL
{
    public class SecurityBL : ISecurityBL
    {
        private readonly ISecurityDAL _securityDAL;
        private readonly IUnitOfWork _unitOfWork;
        public SecurityBL(ISecurityDAL securityDAL, IUnitOfWork unitOfWork)
        {
            _securityDAL = securityDAL;
            _unitOfWork = unitOfWork;
        }
        public Usuarios ChangePassword(Usuarios usuarios, string ContraseñaAnt)
        {
            throw new NotImplementedException();
        }

        public SecurityDTO Login(string Login, string Password)
        {
            try
            {
                string pass = Password;
                Password = _securityDAL.EncriptarSHA256(pass);

                var usuario = _securityDAL.Login(Login, Password);
                SecurityDTO secDTO = new SecurityDTO()
                {
                    UsuarioId = usuario.UsuarioId,
                    NombreUsuario = usuario.Nombre,
                    Login = usuario.Usuario,
                    RolId = usuario.RolId,
                    NombreRol = usuario.Rol.Nombre,
                };
                return secDTO;
            }
            catch (Exception)
            {
                SecurityDTO secDTO = new SecurityDTO();
                return secDTO;
            }
        }
    }
}