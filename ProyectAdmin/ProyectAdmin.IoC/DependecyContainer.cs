using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectAdmin.BL;
using ProyectAdmin.DAL;

namespace ProyectAdmin.IoC
{
	public static class DependecyContainer
	{
		public static IServiceCollection AddProyectDEpendecies(this IServiceCollection services, IConfiguration configuration)
		{
            services.AddDALDependecies(configuration);
            services.AddBLDependecies();
            return services;
		}
	}
}
