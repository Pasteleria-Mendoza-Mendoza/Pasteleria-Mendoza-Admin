using ProyectAdmin.BL.DTOs.RolDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IRolBL
    {
        Task<int> Create(RolInputDTO pRol);

        Task<int> Update(RolInputDTO pRol);

        Task<int> Delete(int id);

        Task<RolSearchingOutputDTO> GetById(int id);

        Task<List<RolSearchingOutputDTO>> Search(RolSearchingInputDTO pRol);
    }
}
