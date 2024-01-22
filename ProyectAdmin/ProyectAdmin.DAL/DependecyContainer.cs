﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public static class DependecyContainer
	{
		public static IServiceCollection AddDALDependecies(this IServiceCollection services, IConfiguration configuration)
        {
			services.AddDbContext<PADbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("conexionLA")));

			services.AddScoped<IAdmin, AdminDAL>();
			services.AddScoped<IBooking, BookingDAL>();
			services.AddScoped<IProduct, ProductDAL>();
			services.AddScoped<IPushesOrder, PushesOrderDAL>();
			services.AddScoped<ISale, SaleDAL>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
		 
	}
}
