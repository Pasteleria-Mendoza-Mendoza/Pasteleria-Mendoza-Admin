﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN
{
    public class Sale
    {
        public int Id { get; set; }
        public string TypeCake { get; set; }
        public string CakeDimensions { get; set; }
        public int ReservationDate { get; set; }
        public int DeliverDate { get; set; }
        public int Cost { get; set; }
    }
}
