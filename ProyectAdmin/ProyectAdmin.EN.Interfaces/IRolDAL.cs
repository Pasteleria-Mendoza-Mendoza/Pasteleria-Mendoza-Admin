using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public void Create(Rol pRol);
    public void Update(Rol pRol);
    public void Delete(Rol pRol);
    public Task<Rol> GetById(int Id);
    public Task<List<Rol>> Search(Rol pRol);
    public Task<List<Rol>> GetAll();
}
