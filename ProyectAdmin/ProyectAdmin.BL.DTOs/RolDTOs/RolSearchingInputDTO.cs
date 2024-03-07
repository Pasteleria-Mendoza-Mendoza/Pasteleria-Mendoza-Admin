using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.RolDTOs
{
    public class RolSearchingInputDTO
    {
        public int RolIdLike { get; set; }
        public string? NombreLike { get; set; }

    }
}
