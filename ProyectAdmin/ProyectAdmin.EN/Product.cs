using System.ComponentModel.DataAnnotations;

namespace ProyectAdmin.EN
{
	public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
