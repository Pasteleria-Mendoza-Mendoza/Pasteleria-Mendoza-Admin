using Microsoft.AspNetCore.Http;


namespace Inventory.Web.Controllers
{
    public class ImageInputDTO  
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime DueDate { get; set; }
        public IFormFile ImageUrl { get; set; }
        public decimal Price { get; set; }

    }
}