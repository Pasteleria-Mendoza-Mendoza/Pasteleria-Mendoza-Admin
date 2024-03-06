using ProyectAdmin.EN;

namespace ProyectAdmin.Data
{
    public class DA_logica
    {
        public List<Admin> listaUsuario()
        {
            return new List<Admin>
            {
                new Admin {Name = "sugey", Email ="administrador@gmail.com",Password ="123" , Roles = new string[]{"Administrador"} }//en esta linea es el error

            };
        }

        public Admin ValidarAdmin(string _email, string _password)
        {
            return listaUsuario().Where(item => item.Email == _email && item.Password == _password). FirstOrDefault();
        }
    }
}
