using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectAdmin.BL.DTOs.AdminDTOs
{
	public class LoginAdminInputDTO
	{
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		//[FromForm(Name = "Password")]
		//[SwaggerSchema(Description = "The user's password.")]
		public string Password { get; set; }

		[DisplayName("Remember Me")]
		public bool RememberMe { get; set; }
	}
}
