using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectAdmin.BL.DTOs.AdminDTOs
{
	public class AdminDTO
	{
		[Required(ErrorMessage = "Name is required")]
		[StringLength(50)]
		[DisplayName("Name")]
		[SwaggerSchema(Description = "The user's name.")]
		public string Name { get; set; }

		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Must contain at least one number, one uppercase letter, one lowercase letter and a special character")]
		[StringLength(50)]
		[MinLength(8, ErrorMessage = "Minimum 8 characters")]
		[MaxLength(15, ErrorMessage = "Maximum 15 characters")]
		[DisplayName("Password")]
		[Required(ErrorMessage = "Password is required")]
		[SwaggerSchema(Description = "The user's password.")]
		public string Password { get; set; }
	}
}
