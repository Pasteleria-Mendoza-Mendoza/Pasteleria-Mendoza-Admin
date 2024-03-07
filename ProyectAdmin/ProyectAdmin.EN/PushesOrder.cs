using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAdmin.EN
{
	public class PushesOrder
    {
        [Key]
        public int IdOrder { get; set; }

        [ForeignKey("Product")]
        public int IdProduct { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Correo { get; set; }
        public string DUI { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int Amount { get; set; }
        public string Dimension { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DeliverDate { get; set; }
        public string Dedication { get; set; }
        public string Details { get; set; }
        public byte State { get; set; }
        public enum StateOrder : byte
        {
            Pendiente = 1,
            Autorizado = 2,
            Rechazado = 3
        }
    }
}

