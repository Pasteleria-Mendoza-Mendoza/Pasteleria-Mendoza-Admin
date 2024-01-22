using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class PushesOrderDAL : IPushesOrder
	{
		readonly PADbContext dbContext;
		public PushesOrderDAL(PADbContext context)
		{
			dbContext = context;
		}

		public void Create(PushesOrder pushesOrder)
		{
			dbContext.Add(pushesOrder);
		}

		public void Delete(PushesOrder pushesOrder)
		{
			dbContext.Remove(pushesOrder);
		}

		public async Task<List<PushesOrder>> GetAll()
		{
			return await dbContext.PushesOrders.ToListAsync();
		}

		public async Task<PushesOrder> GetById(int Id)
		{
			PushesOrder? pushesOrder = await dbContext.PushesOrders.FirstOrDefaultAsync(s => s.Id == Id);
			return pushesOrder;
		}

		public async Task<List<PushesOrder>> Search(PushesOrder pushesOrder)
		{
			IQueryable<PushesOrder> query = dbContext.PushesOrders.AsQueryable();

			if (pushesOrder?.Id is not null && pushesOrder.Id > 0)
				query = query.Where(i => i.Id == pushesOrder.Id);

			if (!string.IsNullOrWhiteSpace(pushesOrder?.CustomerName))
				query = query.Where(n => n.CustomerName.Contains(pushesOrder.CustomerName));

			return await query.ToListAsync();
		}

		public void Update(PushesOrder pushesOrder)
		{
			dbContext.Update(pushesOrder);
		}
	}
}
