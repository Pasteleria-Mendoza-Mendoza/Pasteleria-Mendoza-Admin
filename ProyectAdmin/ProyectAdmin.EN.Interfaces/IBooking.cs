using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface IBooking
    {
        public void Create(Booking booking);
        public void Update(Booking booking);
        public void Delete(Booking booking);
        public Task<List<Booking>> Search(Booking booking);
        public Task<Booking> GetById(int Id);
        public Task<List<Booking>> GetAll();
    }
}
