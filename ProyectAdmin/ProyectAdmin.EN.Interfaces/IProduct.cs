namespace ProyectAdmin.EN.Interfaces
{
	public interface IProduct
    {
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
        Task<List<Product>> Search(Product product);
        Task<Product> GetById(int Id);
        Task<List<Product>> GetAll();
    }
}
