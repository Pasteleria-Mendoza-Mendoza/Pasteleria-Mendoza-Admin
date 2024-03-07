using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface ISecurityDAL
    {
        public string EncriptarSHA256(string str);

        public bool ValidateLogin(Usuarios pUser);

        public Usuarios Login(string Usuario, string Contraseña);

        public Usuarios ChangePassword(Usuarios usuarios, string PasswordAnt);
    }
}

