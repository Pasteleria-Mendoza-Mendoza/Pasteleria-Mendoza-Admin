using ProyectAdmin.BL.DTOs.UsuariosDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.BL.Interfaces
{
    public interface IUsuarioBL
    {
        Task<int> Create(CreateUsuarioDTO pUser);

        Task<int> Update(UsuarioUpdateDTO pUser);

        Task<int> Delete(int Id);

        Task<UsuarioSearchOutputDTO> GetById(int Id);

        Task<List<UsuarioSearchOutputDTO>> Search(UsuarioSearchInputDTO pUser);
    }
}
