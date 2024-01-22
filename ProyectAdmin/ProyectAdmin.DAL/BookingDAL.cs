using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class BookingDAL : IBooking
	{
		readonly PADbContext dbContext;
		public BookingDAL(PADbContext context)
		{
			dbContext = context;
		}
		
		public void Create(Booking booking)
		{
			dbContext.Add(booking);
		}

		public void Delete(Booking booking)
		{
			dbContext.Remove(booking);
		}

		public async Task<List<Booking>> GetAll()
		{
			return await dbContext.Bookings.ToListAsync();
		}

		public async Task<Booking> GetById(int Id)
		{
			Booking? booking = await dbContext.Bookings.FirstOrDefaultAsync(s => s.Id == Id);
			return booking;
		}

		public async Task<List<Booking>> Search(Booking booking)
		{
			IQueryable<Booking> query = dbContext.Bookings.AsQueryable();

			if (booking?.Id is not null && booking.Id > 0)
				query = query.Where(i => i.Id == booking.Id);

			if (!string.IsNullOrWhiteSpace(booking?.CustomerName))
				query = query.Where(n => n.CustomerName.Contains(booking.CustomerName));

			return await query.ToListAsync();
		}

		public void Update(Booking booking)
		{
			dbContext.Update(booking); ;
		}
	}
}
