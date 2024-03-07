using ProyectAdmin.EN;

namespace ProyectAdmin.BL
{
    public interface ISecurityDAL
    {
        public string EncriptarSHA256(string str);

        public bool ValidateLogin(Usuarios pUser);

        public Usuarios Login(string Usuario, string Contraseña);

        public Usuarios ChangePassword(Usuarios usuarios, string PasswordAnt);
    }
}