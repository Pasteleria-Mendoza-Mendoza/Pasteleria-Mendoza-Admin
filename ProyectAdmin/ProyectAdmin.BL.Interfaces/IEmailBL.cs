using ProyectAdmin.BL.DTOs.EmailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IEmailBL
    {
        void EnviarEmail(EmailDTO request);
    }
}
