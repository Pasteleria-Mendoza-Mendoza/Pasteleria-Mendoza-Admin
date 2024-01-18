using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.EN.Interfaces
{
    public interface IPushesOrder
    {
        public void Create(PushesOrder pushesOrder);
        public void Update(PushesOrder pushesOrder);
        public void Delete(PushesOrder pushesOrder);
        public Task<List<PushesOrder>> Search(PushesOrder pushesOrder);
        public Task<PushesOrder> GetById(int Id);
        public Task<List<PushesOrder>> GetAll();
    }
}
