using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		readonly PADbContext _dbContext;

        public UnitOfWork(PADbContext dbContext)
        {
            _dbContext = dbContext;
        }

		public Task<int> SaveChangesAsync()
		{
			return _dbContext.SaveChangesAsync();
		}
	}
}
