using ProyectAdmin.BL.DTOs.PushesOrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IPushesOrderBL
    {
        Task<PushesOrderCreateOutputDTO> Create(PushesOrderCreateInputDTO pushesOrder);
        Task<int> Update(PushesOrderUpdateDTO pushesOrder);
        Task Delete(int id);
        Task<PushesOrderGetByIdDTO> GetById(int id);
        Task<List<PushesOrderSearchOutputDTO>> Search(PushesOrderSearchInputDTO pushesOrder);
    }
}
