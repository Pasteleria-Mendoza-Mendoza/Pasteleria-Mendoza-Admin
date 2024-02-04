using ProyectAdmin.BL.DTOs.PushesOrderDTOs;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IPushesOrderBL
    {
        Task<PushesOrderCreateOutputDTO> Create(PushesOrderCreateInputDTO pushesOrder);
        Task<int> Update(PushesOrderUpdateDTO pushesOrder);
        Task<int> Delete(int id);
        Task<PushesOrderGetByIdDTO> GetById(int id);
        Task<List<PushesOrderSearchOutputDTO>> Search(PushesOrderSearchInputDTO pushesOrder);
    }
}
