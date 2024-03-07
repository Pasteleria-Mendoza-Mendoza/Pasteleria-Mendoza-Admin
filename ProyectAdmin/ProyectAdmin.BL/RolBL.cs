using ProyectAdmin.BL.DTOs.RolDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.BL
{
    public class RolBL : IRolBL

    {
        readonly IRolDAL _rolDAL;
        readonly IUnitOfWork _unitOfWork;

        public RolBL(IRolDAL rolDAL, IUnitOfWork unitOfWork)
        {
            _rolDAL = rolDAL;
            _unitOfWork = unitOfWork;
        }


        public async Task<int> Create(RolInputDTO pRol)
        {
            Rol rolEN = new Rol()
            {
                Nombre = pRol.Nombre,
                Estado = pRol.Estado,
            };
            _rolDAL.Create(rolEN);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            Rol rolEN = await _rolDAL.GetById(Id);
            if (rolEN.RolId == Id)
            {
                _rolDAL.Delete(rolEN);
                return await _unitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<List<RolSearchingOutputDTO>> Search(RolSearchingInputDTO pRol)
        {
            List<Rol> Rol = await _rolDAL.Search(new Rol { RolId = pRol.RolIdLike, Nombre = pRol.NombreLike });
            List<RolSearchingOutputDTO> list = new List<RolSearchingOutputDTO>();
            Rol.ForEach(s => list.Add(new RolSearchingOutputDTO
            {

                RolId = s.RolId,
                Nombre = s.Nombre,
                Estado = s.Estado,

            }));
            return list;

        }

        public async Task<int> Update(RolInputDTO pRol)
        {
            Rol rol = await _rolDAL.GetById(pRol.RolId);
            if (rol.RolId == pRol.RolId)
            {

                rol.RolId = pRol.RolId;
                rol.Nombre = pRol.Nombre;
                rol.Estado = pRol.Estado;
                _rolDAL.Update(rol);
                return await _unitOfWork.SaveChangesAsync();

            }
            else return 0;
        }

        public async Task<RolSearchingOutputDTO> GetById(int Id)
        {
            Rol rolEN = await _rolDAL.GetById(Id);
            return new RolSearchingOutputDTO()
            {

                RolId = rolEN.RolId,
                Nombre = rolEN.Nombre,
                Estado = rolEN.Estado,
            };
        }
    }
}
