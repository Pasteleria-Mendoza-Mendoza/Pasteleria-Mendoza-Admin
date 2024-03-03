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
        Task<ProductCreateOutputDTO> CreateProduct(ProductCreateInputDTO pProductos);
        Task UpdateProduct(ProductUpdateDTO pProductos);
        Task<ProductDeleteDTO> DeleteProduct(ProductDeleteDTO pProductos);
        Task<List<ProductSearchOutputDTO>> Search(ProductSearchOutputDTO pProductos);
        Task<ProductGellAllDTO> SearchOne(ProductGetByIdDTO pProductos);
        Task<string> SubirArchivo(Stream archivo, string nombre);
    }
}
