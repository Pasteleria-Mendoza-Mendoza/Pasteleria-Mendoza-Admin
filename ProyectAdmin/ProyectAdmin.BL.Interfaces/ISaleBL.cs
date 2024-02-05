using ProyectAdmin.BL.DTOs.SaleDTOs;
using ProyectAdmin.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface ISaleBL
    {
        Task<int> Create(SaleAddDTO pSales);
        Task<int> Update(SaleUpdateDTO pSales);
        Task<int> Delete(int id);
        Task<SaleGetByIdDTO> GetById(int id);
        Task<List<SaleGetAllDTO>> GetAll();
        Task<List<SaleSearchOutputDTO>> Search(SaleSearchInputDTO pSales);
    }
}
