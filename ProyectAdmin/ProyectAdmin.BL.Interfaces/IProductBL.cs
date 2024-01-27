using ProyectAdmin.BL.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IProductBL
    {
        Task<int> Create(ProductAddDTO pProducts);
        Task<int> Update(ProductUpdateDTO pProducts);
        Task<int> Delete(int id);
        Task<ProductGetByIdDTO> GetById(int id);
        Task<List<ProductGellAllDTO>> GetAll();
        Task<List<ProductSearchOutputDTO>> Search(ProductSearchInputDTO pProducts);
    }
}
