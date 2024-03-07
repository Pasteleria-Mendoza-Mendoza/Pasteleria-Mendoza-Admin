
using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;
using System.Security.Cryptography;
using System.Text;


namespace ProyectAdmin.DAL
{
    public class SecurityDAL : ISecurityDAL
    {
        readonly PADbContext dbContext;
        public SecurityDAL(PADbContext context)
        {
            dbContext = context;
        }
        #region SEGURIDAD

        public string EncriptarSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public bool ValidateLogin(Usuarios pUser)
        {
            bool result = false;
            var loginUsuarioExiste = dbContext.Usuarios.FirstOrDefault(s => s.Usuario == pUser.Usuario && s.UsuarioId != pUser.UsuarioId);
            if (loginUsuarioExiste != null && loginUsuarioExiste.UsuarioId > 0 && loginUsuarioExiste.Usuario == pUser.Usuario)
                result = true;
            return result;
        }

        public Usuarios Login(string Login, string Password)
        {
            try
            {
                Usuarios pUsers = new Usuarios();
                pUsers = dbContext.Usuarios.Include(s => s.Rol).AsQueryable().FirstOrDefault(s => s.Usuario == Login && s.Contraseña == Password && s.Estado == (byte)EstadoUsuario.Activo);
                if (pUsers is null)
                    throw new ArgumentException("El usuario no Existe");
                else
                    return pUsers;

            }
            catch (Exception)
            {
                Usuarios user = new Usuarios();
                return user;
            }
        }
        public Usuarios ChangePassword(Usuarios usuarios, string PasswordAnt)
        {
            try
            {
                int result = 0;
                var passwordAnt = EncriptarSHA256(PasswordAnt);

                Usuarios pUsers = dbContext.Usuarios.FirstOrDefault(s => s.UsuarioId == usuarios.UsuarioId);

                if (passwordAnt != pUsers.Contraseña.Trim())
                    throw new ArgumentException("El password actual es incorrecto");

                pUsers.Contraseña = EncriptarSHA256(usuarios.Contraseña);
                dbContext.Usuarios.Update(pUsers);
                result = dbContext.SaveChanges();

                return pUsers;
            }
            catch (Exception e)
            {
                return usuarios;
            }
        }

        #endregion
    }
}
