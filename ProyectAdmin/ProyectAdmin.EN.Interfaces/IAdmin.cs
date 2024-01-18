using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface IAdmin
    {
        public void Create(Admin admin);
        public void Update(Admin admin);
        public void Delete(Admin admin);
        public Task<List<Admin>> Search(Admin admin);
        public Task<Admin> GetById(int Id);
        public Task<List<Admin>> GetAll();
    }
}
