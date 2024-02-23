﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.ProductDTOs
{
    public class ProductAddDTO
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}