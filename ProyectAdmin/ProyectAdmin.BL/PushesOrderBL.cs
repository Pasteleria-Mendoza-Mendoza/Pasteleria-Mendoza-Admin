using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;
using ProyectAdmin.BL.DTOs.PushesOrderDTOs;

namespace ProyectAdmin.BL
{
    public class PushesOrderBL : IPushesOrderBL
    {
        readonly IPushesOrder xpushesOrderDAL;
        readonly IUnitOfWork xunitOfWork;

        public PushesOrderBL(IPushesOrder pushesOrderDAL, IUnitOfWork unitOfWork)
        {
            xpushesOrderDAL = pushesOrderDAL;
            xunitOfWork = unitOfWork;
        }

        public async Task<PushesOrderCreateOutputDTO> Create(PushesOrderCreateInputDTO pushesOrder)
        {
            PushesOrder pushesOrders = new PushesOrder
            {
                Id = pushesOrder.Id,
                CustomerName = pushesOrder.CustomerName,
                ContactNumber = pushesOrder.ContactNumber,
                CakeQuantity = pushesOrder.CakeQuantity,
                CakeDimensions = pushesOrder.CakeDimensions,
                CakeDedication = pushesOrder.CakeDedication,
                ReservationDate = pushesOrder.ReservationDate,
                CakeCost = pushesOrder.CakeCost,
                State = pushesOrder.State
            };

            xpushesOrderDAL.Create(pushesOrders);
            await xunitOfWork.SaveChangesAsync();
            PushesOrderCreateOutputDTO pushesOrdersOutput = new PushesOrderCreateOutputDTO
            {
                Id = pushesOrder.Id,
                CustomerName = pushesOrder.CustomerName,
                ContactNumber = pushesOrder.ContactNumber,
                CakeQuantity = pushesOrder.CakeQuantity,
                CakeDimensions = pushesOrder.CakeDimensions,
                CakeDedication = pushesOrder.CakeDedication,
                ReservationDate = pushesOrder.ReservationDate,
                CakeCost = pushesOrder.CakeCost,
                State = pushesOrder.State
            };
            return pushesOrdersOutput;
        }

        public async Task<int> Delete(int id)
        {
            PushesOrder pushesOrders = await xpushesOrderDAL.GetById(id);
            if (pushesOrders.Id == id)
            {
                xpushesOrderDAL.Delete(pushesOrders);
            }
            else
                return 0;
        }

        public async Task<PushesOrderGetByIdDTO> GetById(int id)
        {
            PushesOrder pushesOrders = await xpushesOrderDAL.GetById(id);
            return new PushesOrderGetByIdDTO()
            {
                Id = pushesOrders.Id
            };
        } 

        public async Task<List<PushesOrderSearchOutputDTO>> Search(PushesOrderSearchInputDTO pushesOrder)
        {
            List<PushesOrder> pushesOrders = await xpushesOrderDAL.Search(new PushesOrder { CustomerName = pushesOrder.CustomerName });
            List<PushesOrderSearchOutputDTO> list = new List<PushesOrderSearchOutputDTO>();
            pushesOrders.ForEach(s => list.Add(new PushesOrderSearchOutputDTO
            {
                CustomerName = s.CustomerName,
                ContactNumber = s.ContactNumber,
                CakeQuantity = s.CakeQuantity,
                CakeDimensions = s.CakeDimensions,
                CakeDedication = s.CakeDedication,
                ReservationDate = s.ReservationDate,
                CakeCost = s.CakeCost,
                State = s.State
            }));
        }

        public async Task<int> Update(PushesOrderUpdateDTO pushesOrder)
        {
            PushesOrder pushesOrders = await xpushesOrderDAL.Update(pushesOrder.CustomerName);
            if (pushesOrders.CustomerName == pushesOrder.CustomerName)
            {
                pushesOrders.CustomerName = pushesOrder.CustomerName;
                pushesOrders.ContactNumber = pushesOrder.ContactNumber;
                pushesOrders.CakeQuantity = pushesOrder.CakeQuantity;
                pushesOrders.CakeDimensions = pushesOrder.CakeDimensions;
                pushesOrders.CakeDedication = pushesOrder.CakeDedication;
                pushesOrders.ReservationDate = pushesOrder.ReservationDate;
                pushesOrders.CakeCost = pushesOrder.CakeCost;
                pushesOrders.State = pushesOrder.State;
            }
        }
    }
}
