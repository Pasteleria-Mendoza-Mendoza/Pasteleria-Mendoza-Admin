using ProyectAdmin.BL.DTOs.ProductDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.DAL;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL
{
    public class ProductBL : IProductBL
    {
        readonly IProduct xproductDAL;
        readonly IUnitOfWork xunitOfWork;

        public ProductBL(IProduct productDAL, IUnitOfWork unitOfWork)
        {
            xproductDAL = productDAL;
            xunitOfWork = unitOfWork;
        }

        public async Task<int> Create(ProductAddDTO pProducts)
        {
            Product product = new Product
            {
                Id = pProducts.Id,
                Name = pProducts.Name,
                Quantity = pProducts.Quantity,
                Dimensions = pProducts.Dimensions,
                AcquisitionDate = pProducts.AcquisitionDate,
                DueDate = pProducts.DueDate,
            };
            xproductDAL.Create(product);
            return await xunitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Product products = await xproductDAL.GetById(id);
            if (products.Id == id)
            {
                xproductDAL.Delete(products);
                return await xunitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<List<ProductGellAllDTO>> GetAll()
        {
            List<Product> products = await xproductDAL.GetAll();
            List<ProductGellAllDTO> list = new List<ProductGellAllDTO>();
            products.ForEach(p => list.Add(new ProductGellAllDTO
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Dimensions = p.Dimensions,
                AcquisitionDate = p.AcquisitionDate,
                DueDate = p.DueDate
            }));
            return list;
        }

        public async Task<ProductGetByIdDTO> GetById(int id)
        {
            Product products = await xproductDAL.GetById(id);
            return new ProductGetByIdDTO()
            {
                Id = products.Id,
                Name = products.Name,
                Quantity = products.Quantity,
                Dimensions = products.Dimensions,
                AcquisitionDate = products.AcquisitionDate,
                DueDate = products.DueDate
            };
        }

        public async Task<List<ProductSearchOutputDTO>> Search(ProductSearchInputDTO pProducts)
        {
            List<Product> products = await xproductDAL.Search(new Product { Name = pProducts.Name });
            List<ProductSearchOutputDTO> list = new List<ProductSearchOutputDTO>();
            products.ForEach(p => list.Add(new ProductSearchOutputDTO
            {
                Id = p.Id,
                Name = p.Name,
            }));
            return list;
        }

        public async Task<int> Update(ProductUpdateDTO pProducts)
        {
            Product products = await xproductDAL.GetById(pProducts.Id);
            if (products.Id == pProducts.Id)
            {
                products.Name = pProducts.Name;
                xproductDAL.Update(products);
                return await xunitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }
    }
}
