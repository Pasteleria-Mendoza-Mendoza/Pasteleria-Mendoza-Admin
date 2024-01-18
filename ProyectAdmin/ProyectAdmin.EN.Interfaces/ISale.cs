using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface ISale
    {
        public void Create(Sale sale);
        public void Update(Sale sale);
        public void Delete(Sale sale);
        public Task<List<Sale>> Search(Sale sale);
        public Task<Sale> GetById(int Id);
        public Task<List<Sale>> GetAll();
    }
}
