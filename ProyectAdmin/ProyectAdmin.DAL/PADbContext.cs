using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;

namespace ProyectAdmin.DAL
{
	public class PADbContext : DbContext
	{
		public PADbContext(DbContextOptions<PADbContext> options) : base(options) { }

		public DbSet<Admin> Admins { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<PushesOrder> PushesOrder { get; set; }
		public DbSet<Sale> Sales { get; set; }
        public DbSet<Rol> Rol { get; set; }

    }
}
