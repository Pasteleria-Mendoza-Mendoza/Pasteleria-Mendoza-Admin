namespace ProyectAdmin.EN.Interfaces
{
	public interface IAdmin
    {
        void Create(Admin admin);
        void Update(Admin admin);
        void Delete(Admin admin);
        Task<List<Admin>> Search(Admin admin);
        Task<Admin> GetById(int Id);
        Task<Admin> Login(Admin admin);
    }
}
