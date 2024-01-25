using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.BookingDTOs
{
    public class BookingAddDTO
    {
        public string CustomerName { get; set; }
        public int ContactNumber { get; set; }
        public int CakeQuantity { get; set; }
        public string CakeDimensions { get; set; }
        public string CakeDedication { get; set; }
        public int ReservationShipping {  get; set; }
        public int ReservationDate { get; set; }
        public int CakeCost { get; set; }
    }
}
