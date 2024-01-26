using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectAdmin.BL.DTOs.AdminDTOs
{
	public class AdminGetAllDTO
	{
		[Required(ErrorMessage = "Name is required")]
		[StringLength(50)]
		[DisplayName("Name")]
		[SwaggerSchema(Description = "The user's name.")]
		public string Name { get; set; }
	}
}
