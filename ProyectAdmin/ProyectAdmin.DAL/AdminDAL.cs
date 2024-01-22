using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class AdminDAL : IAdmin
	{
		readonly PADbContext dbContext;
		public AdminDAL(PADbContext context)
		{
			dbContext = context;
		}

		public void Create(Admin admin)
		{
			dbContext.Add(admin);
		}

		public void Delete(Admin admin)
		{
			dbContext.Remove(admin);
		}

		public async Task<List<Admin>> GetAll()
		{
			return await dbContext.Admins.ToListAsync();
		}

		public async Task<Admin> GetById(int Id)
		{
			Admin? admin = await dbContext.Admins.FirstOrDefaultAsync(s => s.Id == Id);
			return admin;
		}

		public async Task<List<Admin>> Search(Admin admin)
		{
			IQueryable<Admin> query = dbContext.Admins.AsQueryable();

			if (admin?.Id is not null && admin.Id > 0)
				query = query.Where(i => i.Id == admin.Id);

			if (!string.IsNullOrWhiteSpace(admin?.Name))
				query = query.Where(n => n.Name.Contains(admin.Name));

			return await query.ToListAsync();
		}

		public void Update(Admin admin)
		{
			dbContext.Update(admin);
		}
	}
}
