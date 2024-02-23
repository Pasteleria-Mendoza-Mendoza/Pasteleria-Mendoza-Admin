using ProyectAdmin.BL.DTOs.OrdersDTOs;
using ProyectAdmin.BL.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs
{
    public class ProductOrderViewModel
    {
        public ProductGellAllDTO Product { get; set; }
        public CreateOrderInputDTO Order { get; set; }
    }
}
