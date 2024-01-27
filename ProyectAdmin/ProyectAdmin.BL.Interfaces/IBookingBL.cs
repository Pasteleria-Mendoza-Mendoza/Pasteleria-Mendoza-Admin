using ProyectAdmin.BL.DTOs.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IBookingBL
    {
        Task<BookingCreateOutputDTO> Create(BookingCreateInputDTO pBookings);
        Task<int> Create(BookingAddDTO pBookings);
    }
}
