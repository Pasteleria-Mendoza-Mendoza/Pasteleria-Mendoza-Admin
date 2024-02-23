using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class ProductDAL : IProduct
	{
		readonly PADbContext dbContext;
		public ProductDAL(PADbContext context)
		{
			dbContext = context;
		}

        public void Create(Product pProductos)
        {
            dbContext.Products.Add(pProductos);
        }

        public void Delete(Product pProductos)
        {
            dbContext.Products.Remove(pProductos);
        }

        public async Task<List<Product>> Get(Product pProductos)
        {
            List<Product> isProductos = await dbContext.Products.ToListAsync();
            return isProductos;
        }

        public async Task<Product> GetOne(int productoId)
        {
            Product isProductos = await dbContext.Products.FindAsync(productoId);
            return isProductos;
        }

        public void Update(Product pProductos)
        {
            dbContext.Products.Update(pProductos);
        }
        public async Task<Product> GetByName(Product pProductos)
        {
            Product isProductos = await dbContext.Products.SingleOrDefaultAsync(p => p.NameProduct == pProductos.NameProduct);
            return isProductos;
        }
    }
}
