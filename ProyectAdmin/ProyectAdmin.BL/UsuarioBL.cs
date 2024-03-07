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
    public class UsuarioBL : IUsuarioBL
    {
        private readonly IUsuarioDAL _usuarioDAL;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityDAL _securityDAL;

        public UsuarioBL(IUsuarioDAL usuarioDAL, IUnitOfWork unitOfWork, ISecurityDAL securityDAL)
        {

            _usuarioDAL = usuarioDAL;
            _unitOfWork = unitOfWork;
            _securityDAL = securityDAL;
        }
        public async Task<int> Create(CreateUsuarioDTO pUser)
        {
            try
            {

                Usuarios user = new Usuarios
                {
                    RolId = pUser.RolId,
                    Nombre = pUser.Nombre,
                    Apellido = pUser.Apellido,
                    Correo = pUser.Correo,
                    Usuario = pUser.Usuario,
                    Contraseña = pUser.Contraseña, // Función para hashear la contraseña
                    Estado = pUser.Estado,
                };

                #region VALIDAR LOGIN Y ENCRIPTAR PASSWORD
                bool ExisteLogin = _securityDAL.ValidateLogin(user);
                if (ExisteLogin)
                    throw new ArgumentException("El Login ya existe, Intente uno diferente");
                string pass = user.Contraseña;
                user.Contraseña = _securityDAL.EncriptarSHA256(pass);
                #endregion


                _usuarioDAL.Create(user);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                    return result; // Devuelve el ID del usuario creado o un mensaje de éxito
                return 0;
            }
            catch (Exception e)
            {
                // Registra la excepción o lanza una nueva excepción con un mensaje significativo.
                return 0;
            }
        }

        public async Task<int> Delete(int Id)
        {
            Usuarios UserEn = await _usuarioDAL.GetById(Id);
            if (UserEn.UsuarioId == Id)
            {
                _usuarioDAL.Delete(UserEn);
                return await _unitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<UsuarioSearchOutputDTO> GetById(int Id)
        {
            Usuarios UserEn = await _usuarioDAL.GetById(Id);
            return new UsuarioSearchOutputDTO()
            {
          
                UsuarioId = UserEn.UsuarioId,
                RolId = UserEn.RolId,
                Nombre = UserEn.Nombre,
                Apellido = UserEn.Apellido,
                Correo = UserEn.Correo,
                Usuario = UserEn.Usuario,
                Estado = UserEn.Estado
            };

        }

        public async Task<List<UsuarioSearchOutputDTO>> Search(UsuarioSearchInputDTO pUser)
        {
            List<Usuarios> usuarios = await _usuarioDAL.Search(new Usuarios { Nombre = pUser.NombreLike });
            List<UsuarioSearchOutputDTO> list = new List<UsuarioSearchOutputDTO>();
            usuarios.ForEach(s => list.Add(new UsuarioSearchOutputDTO
            {
                UsuarioId = s.UsuarioId,
                RolId = s.RolId,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Correo = s.Correo,
                Estado = s.Estado,
                Usuario = s.Usuario,
                NombreRol = s.Rol.Nombre,
            }));
            return list;
        }

        public async Task<int> Update(UsuarioUpdateDTO pUser)
        {
            try
            {
                Usuarios UserEn = await _usuarioDAL.GetById(pUser.UsuarioId);

                if (UserEn == null)
                {
                    // Manejar el caso en que el usuario no se encuentre
                    return 0;
                }

                // Actualiza los datos del usuario
                UserEn.RolId = pUser.RolId;
                UserEn.Nombre = pUser.Nombre;
                UserEn.Apellido = pUser.Apellido;
                UserEn.Correo = pUser.Correo;
                UserEn.Usuario = pUser.Usuario;
                UserEn.Estado = (byte)pUser.Estado;

                _usuarioDAL.Update(UserEn);
                return await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // Manejar excepciones según sea necesario
                throw;
            }
        }
    }
}