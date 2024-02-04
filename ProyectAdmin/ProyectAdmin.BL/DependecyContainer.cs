using Microsoft.Extensions.DependencyInjection;
using ProyectAdmin.BL.Interfaces;

namespace ProyectAdmin.BL
{
	public static class DependecyContainer
	{
		public static IServiceCollection AddBLDependecies(this IServiceCollection services)
		{
			services.AddTransient<IAdminBL, AdminBL>();
			services.AddTransient<IBookingBL, BookingBL>();
			services.AddTransient<IPushesOrderBL, PushesOrderBL>();
			services.AddTransient<IProductBL, ProductBL>();
			services.AddTransient<ISaleBL, SaleBL>();
			return services;
		}
	}
}
