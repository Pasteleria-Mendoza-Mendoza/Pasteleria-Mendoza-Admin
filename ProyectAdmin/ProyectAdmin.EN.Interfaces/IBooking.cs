namespace ProyectAdmin.EN.Interfaces
{
	public interface IBooking
    {
        void Create(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
        Task<List<Booking>> Search(Booking booking);
        Task<Booking> GetById(int Id);
        Task<List<Booking>> GetAll();
    }
}
