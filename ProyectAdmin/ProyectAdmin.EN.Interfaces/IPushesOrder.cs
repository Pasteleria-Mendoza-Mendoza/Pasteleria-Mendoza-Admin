namespace ProyectAdmin.EN.Interfaces
{
	public interface IPushesOrder
    {
        void Create(PushesOrder pOrder);
        Task<List<PushesOrder>> Search();
        Task<PushesOrder> GetById(int id);
        Task<List<PushesOrder>> SearchByDate(DateTime date);
        Task DeleteOrden(int ordenId);

        //METODOS PARA AUTORIZAR Y ACTUALIZAR
        Task AutorizarOrden(int ordenId);
        Task RechazarOrdenAsync(int ordenId);
        Task ActualizarStock(int productoId, int cantidad);
    }
}
