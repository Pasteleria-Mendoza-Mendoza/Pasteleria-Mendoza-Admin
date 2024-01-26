using ProyectAdmin.BL.DTOs.AdminDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ProyectAdmin.BL
{
	public class AdminBL : IAdminBL
	{
		readonly IAdmin adminDAL;
        readonly IUnitOfWork unitWork;

        public AdminBL(IAdmin pAdminDAL, IUnitOfWork pUnitWork)
        {
            adminDAL = pAdminDAL;
            unitWork = pUnitWork;
        }

		public Task<int> Create(AdminAddDTO pAdmin)
		{
			throw new NotImplementedException();
		}

		public Task<int> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<AdminGetAllDTO>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<AdminDTO> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<LoginAdminOutputDTO> Login(LoginAdminInputDTO login)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// //////////////////////////////////////////////////////
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		private static string EncryptMD5(string password)
		{
			using var md5 = MD5.Create();
			var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
			var strEncriptar = "";
			for (int i = 0; i < result.Length; i++)
				strEncriptar += result[i].ToString("x2").ToLower();
			return strEncriptar;
		}

		public Task<int> Update(int id, AdminUpdateDTO admin)
		{
			throw new NotImplementedException();
		}
	}
}
