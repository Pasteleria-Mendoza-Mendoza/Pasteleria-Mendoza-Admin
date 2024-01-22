using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectAdmin.DAL;

namespace ProyectAdmin.IoC
{
	public static class DependecyContainer
	{
		public static IServiceCollection AddProyectDEpendecies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDALDependecies(configuration);
			//services.AddBLDependecies(); Esto es de las BL, cuando las termines quitas el comentario.
			return services;
		}
	}
}
