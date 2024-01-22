namespace ProyectAdmin.EN.Interfaces
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
	}
}
