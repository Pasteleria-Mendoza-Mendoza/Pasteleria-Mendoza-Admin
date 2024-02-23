namespace ProyectAdmin.BL.DTOs.ProductDTOs
{
    public class ProductCreateInputDTO
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
