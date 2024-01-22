namespace ProyectAdmin.EN.Interfaces
{
	public interface ISale
    {
        void Create(Sale sale);
        void Update(Sale sale);
        void Delete(Sale sale);
        Task<List<Sale>> Search(Sale sale);
        Task<Sale> GetById(int Id);
        Task<List<Sale>> GetAll();
    }
}
