using ProyectAdmin.BL.DTOs.OrdersDTOs;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IPushesOrderBL
    {
        public Task<GetAllOrderOutputDTO> GetOrderById(int Id);
        public Task<List<GetAllOrderOutputDTO>> GetAllOrder(DateTime? specificDate = null);
        public Task<CreateOrderInputDTO> AddOrder(CreateOrderInputDTO orden);
        Task DeleteOrden(int ordeinId);
        Task AutorizarPedidoAsync(int ordenId);
        Task RechazarPedidoAsync(int ordenId);
    }
}
