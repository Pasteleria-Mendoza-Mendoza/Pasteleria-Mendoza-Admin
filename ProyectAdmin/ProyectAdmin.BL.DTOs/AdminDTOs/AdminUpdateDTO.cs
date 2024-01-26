using System.ComponentModel.DataAnnotations;

namespace ProyectAdmin.BL.DTOs.AdminDTOs
{
	public class AdminUpdateDTO
	{
		[StringLength(50)]
		//[FromForm(Name = "Name")]
		//[SwaggerSchema(Description = "The user's name.")]
		public string? Name { get; set; }

		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Must contain at least one number, one uppercase letter, one lowercase letter and a special character")]
		[StringLength(50)]
		[MinLength(8, ErrorMessage = "Minimum 8 characters")]
		[MaxLength(15, ErrorMessage = "Maximum 15 characters")]
		//[FromForm(Name = "Password")]
		//[SwaggerSchema(Description = "Password.")]
		public string? Password { get; set; }
	}
}
