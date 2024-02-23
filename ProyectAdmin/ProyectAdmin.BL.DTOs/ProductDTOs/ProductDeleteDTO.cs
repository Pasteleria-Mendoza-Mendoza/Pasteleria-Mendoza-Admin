using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.ProductDTOs
{
    public class ProductDeleteDTO
    {
        public int IdProduct { get; set; }
        public bool IsDeleted { get; set; }
    }
}
