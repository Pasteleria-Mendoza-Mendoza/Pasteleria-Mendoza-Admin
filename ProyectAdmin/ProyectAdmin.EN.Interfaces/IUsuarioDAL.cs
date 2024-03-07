using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface IUsuarioDAL
    {
        public void Create(Usuarios pUsuarios);
        public void Update(Usuarios pUsuarios);
        public void Delete(Usuarios pUsuarios);
        public Task<Usuarios> GetById(int Id);
        public Task<List<Usuarios>> Search(Usuarios pUsuarios);

    }
}
