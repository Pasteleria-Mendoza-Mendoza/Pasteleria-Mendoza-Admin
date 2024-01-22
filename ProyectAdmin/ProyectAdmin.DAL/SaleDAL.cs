using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class SaleDAL : ISale
	{
		readonly PADbContext dbContext;
		public SaleDAL(PADbContext context)
		{
			dbContext = context;
		}

		public void Create(Sale sale)
		{
			dbContext.Add(sale);
		}

		public void Delete(Sale sale)
		{
			dbContext.Remove(sale);
		}

		public async Task<List<Sale>> GetAll()
		{
			return await dbContext.Sales.ToListAsync();
		}

		public async Task<Sale> GetById(int Id)
		{
			Sale? sale = await dbContext.Sales.FirstOrDefaultAsync(s => s.Id == Id);
			return sale;
		}

		public async Task<List<Sale>> Search(Sale sale)
		{
			IQueryable<Sale> query = dbContext.Sales.AsQueryable();

			if (sale?.Id is not null && sale.Id > 0)
				query = query.Where(i => i.Id == sale.Id);

			if (!string.IsNullOrWhiteSpace(sale?.TypeCake))
				query = query.Where(n => n.TypeCake.Contains(sale.TypeCake));

			return await query.ToListAsync();
		}

		public void Update(Sale sale)
		{
			dbContext.Update(sale);
		}
	}
}
