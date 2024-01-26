using ProyectAdmin.BL.DTOs.AdminDTOs;

namespace ProyectAdmin.BL.Interfaces
{
	public interface IAdminBL
	{
		Task<List<AdminGetAllDTO>> GetAll();
		Task<int> Create(AdminAddDTO pAdmin);
		Task<int> Update(int id, AdminUpdateDTO admin);
		Task<int> Delete(int id);
		Task<AdminDTO> GetById(int id);
		Task<LoginAdminOutputDTO> Login(LoginAdminInputDTO pAdmin);
	}
}
