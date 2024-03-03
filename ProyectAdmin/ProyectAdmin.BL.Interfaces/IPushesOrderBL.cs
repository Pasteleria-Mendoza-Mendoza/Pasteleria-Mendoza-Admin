using ProyectAdmin.BL.DTOs.OrdersDTOs;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IPushesOrderBL
    {
        Task<GetAllOrderOutputDTO> GetOrderById(GetAllOrderOutputDTO pProductos);
        Task<List<GetAllOrderOutputDTO>> GetAllOrder(DateTime? specificDate = null);
        Task<CreateOrderInputDTO> AddOrder(CreateOrderInputDTO orden);
        Task DeleteOrden(int ordeinId);
        Task AutorizarPedidoAsync(int ordenId);
        Task RechazarPedidoAsync(int ordenId);
    }
}
