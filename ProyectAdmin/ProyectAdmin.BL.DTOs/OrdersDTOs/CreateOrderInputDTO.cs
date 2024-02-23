using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectAdmin.EN.PushesOrder;

namespace ProyectAdmin.BL.DTOs.OrdersDTOs
{
    public class CreateOrderInputDTO
    {
        public int IdProduct { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string DUI { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int Amount { get; set; }
        public string Dimension { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DeliverDate { get; set; }
        public string Dedication { get; set; }
        public string Details { get; set; }
        public StateOrder State { get; set; }
        public decimal Cost { get; set; }
    }
}
