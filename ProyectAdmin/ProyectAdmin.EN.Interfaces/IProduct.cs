using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface IProduct
    {
        public void Create(Product product);
        public void Update(Product product);
        public void Delete(Product product);
        public Task<List<Product>> Search(Product product);
        public Task<Product> GetById(int Id);
        public Task<List<Product>> GetAll();
    }
}
