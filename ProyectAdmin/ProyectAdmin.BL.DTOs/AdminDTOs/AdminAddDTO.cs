using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ProyectAdmin.BL.DTOs.AdminDTOs
{
	public class AdminAddDTO
	{
		[Required(ErrorMessage = "Name is required")]
		[StringLength(50)]
		[FromForm(Name = "Name")]
		[SwaggerSchema(Description = "The user's name.")]
		public string Name { get; set; }


		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$", ErrorMessage = "Must contain at least one number, one uppercase letter, one lowercase letter and a special character")]
		[StringLength(50)]
		[MinLength(8, ErrorMessage = "Minimum 8 characters")]
		[MaxLength(32, ErrorMessage = "Maximum 15 characters")]
		[FromForm(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]
		[SwaggerSchema(Description = "The user's password.")]
		public string Password { get; set; }
	}
}
