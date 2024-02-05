using ProyectAdmin.BL.DTOs.SaleDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.BL
{
    public class SaleBL : ISaleBL
    {
        readonly ISale xSaleDAL;
        readonly IUnitOfWork xUnitOfWork;

        public SaleBL(ISale saleDAL, IUnitOfWork unitOfWork)
        {
            xSaleDAL = saleDAL;
            xUnitOfWork = unitOfWork;
        }

        public async Task<int> Create(SaleAddDTO pSales)
        {
            Sale sales = new Sale
            {
                Id = pSales.Id,
                TypeCake = pSales.TypeCake,
                CakeDimensions = pSales.CakeDimensions,
                ReservationDate = pSales.ReservationDate,
                DeliverDate = pSales.DeliverDate,
                Cost = pSales.Cost,
            };
            xSaleDAL.Create(sales);
            return await xUnitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Sale sales = await xSaleDAL.GetById(id);
            if (sales.Id == id)
            {
                xSaleDAL.Delete(sales);
                return await xUnitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<List<SaleGetAllDTO>> GetAll()
        {
            List<Sale> sales = await xSaleDAL.GetAll();
            List<SaleGetAllDTO> list = new List<SaleGetAllDTO>();
            sales.ForEach(s => list.Add(new SaleGetAllDTO
            {
                Id=s.Id,
                TypeCake = s.TypeCake,
                CakeDimensions = s.CakeDimensions,
                ReservationDate = s.ReservationDate,
                DeliverDate = s.DeliverDate,
                Cost = s.Cost
            }));
            return list;
        }

        public async Task<SaleGetByIdDTO> GetById(int id)
        {
            Sale sales = await xSaleDAL.GetById(id);
            return new SaleGetByIdDTO()
            {
                Id = sales.Id,
                TypeCake = sales.TypeCake,
                CakeDimensions = sales.CakeDimensions,
                ReservationDate = sales.ReservationDate,
                DeliverDate = sales.DeliverDate,
                Cost = sales.Cost
            };
        }

        public async Task<List<SaleSearchOutputDTO>> Search(SaleSearchInputDTO pSales)
        {
            List<Sale> sales = await xSaleDAL.Search(new Sale { TypeCake = pSales.TypeCake, });
            List<SaleSearchOutputDTO> list = new List<SaleSearchOutputDTO>();
            sales.ForEach(s => list.Add(new SaleSearchOutputDTO
            {
                TypeCake = s.TypeCake,
            }));
            return list;
        }

        public async Task<int> Update(SaleUpdateDTO pSales)
        {
            Sale sales = await xSaleDAL.GetById(pSales.Id);
            if (sales.Id == pSales.Id)
            {
                sales.TypeCake = pSales.TypeCake;
                sales.CakeDimensions = pSales.CakeDimensions;
                sales.ReservationDate = pSales.ReservationDate;
                sales.DeliverDate = pSales.DeliverDate;
                sales.Cost = pSales.Cost;
                xSaleDAL.Update(sales);
                return await xUnitOfWork.SaveChangesAsync();
            }
            else
                return 0;

        }
    }
}
