using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN.Interfaces;
using ProyectAdmin.DAL;
using ProyectAdmin.EN;
using ProyectAdmin.BL.DTOs.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL
{
    public class BookingBL : IBookingBL
    {
        readonly IBooking xbookingDAL;
        readonly IUnitOfWork xunitOfWork;

        public BookingBL(IBooking bookingDAL, IUnitOfWork unitOfWork)
        {
            xbookingDAL = bookingDAL;
            xunitOfWork = unitOfWork;
        }

        public async Task<int> Create(BookingAddDTO pBookings)
        {
            Booking xbooking = new Booking
            {
                Id = pBookings.Id,
                CustomerName = pBookings.CustomerName,
                ContactNumber = pBookings.ContactNumber,
                CakeQuantity = pBookings.CakeQuantity,
                CakeDimensions = pBookings.CakeDimensions,
                CakeDedication = pBookings.CakeDedication,
                ReservationShipping = pBookings.ReservationShipping,
                ReservationDate = pBookings.ReservationDate,
                CakeCost = pBookings.CakeCost,
            };
            xbookingDAL.Create(xbooking);
            return await xunitOfWork.SaveChangesAsync();
        }
    }
}
