namespace ProyectAdmin.EN.Interfaces
{
	public interface IProduct
    {
        void Create(Product pProductos);
        void Update(Product pProductos);
        void Delete(Product pProductos);
        Task<Product> GetOne(int productoId);
        Task<List<Product>> Get(Product pProductos);
        Task<Product> GetByName(Product pProductos);

    }
}
