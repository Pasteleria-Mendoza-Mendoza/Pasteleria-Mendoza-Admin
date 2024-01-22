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

		public void Create(Product product)
		{
			dbContext.Add(product);
		}

		public void Delete(Product product)
		{
			dbContext.Remove(product);
		}

		public async Task<List<Product>> GetAll()
		{
			return await dbContext.Products.ToListAsync();
		}

		public async Task<Product> GetById(int Id)
		{
			Product? product = await dbContext.Products.FirstOrDefaultAsync(s => s.Id == Id);
			return product;
		}

		public async Task<List<Product>> Search(Product product)
		{
			IQueryable<Product> query = dbContext.Products.AsQueryable();

			if (product?.Id is not null && product.Id > 0)
				query = query.Where(i => i.Id == product.Id);

			if (!string.IsNullOrWhiteSpace(product?.Name))
				query = query.Where(n => n.Name.Contains(product.Name));

			return await query.ToListAsync();
		}

		public void Update(Product product)
		{
			dbContext.Update(product); ;
		}
	}
}
