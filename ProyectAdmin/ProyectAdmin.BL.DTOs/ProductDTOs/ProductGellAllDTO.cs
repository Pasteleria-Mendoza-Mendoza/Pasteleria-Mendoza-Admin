using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.ProductDTOs
{
    public class ProductGellAllDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public int AcquisitionDate { get; set; }
        public int DueDate { get; set; }
    }
}
