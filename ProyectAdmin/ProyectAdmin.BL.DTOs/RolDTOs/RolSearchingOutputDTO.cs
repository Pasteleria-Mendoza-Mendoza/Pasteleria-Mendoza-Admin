using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.DTOs.RolDTOs
{
    public class RolSearchingOutputDTO
    {
        public int RolId { get; set; }
        public string? Nombre { get; set; }

        public byte Estado { get; set; }
        string strEstado = "";
        public String StrStatus
        {
            get
            {
                if (Estado == 1)
                {
                    strEstado = "ACTIVO";
                }
                else
                {
                    strEstado = "INACTIVO";
                }
                return strEstado;
            }
        }

        public object Rol { get; set; }
    }
}
