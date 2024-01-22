namespace ProyectAdmin.EN.Interfaces
{
	public interface IPushesOrder
    {
        void Create(PushesOrder pushesOrder);
        void Update(PushesOrder pushesOrder);
        void Delete(PushesOrder pushesOrder);
        Task<List<PushesOrder>> Search(PushesOrder pushesOrder);
        Task<PushesOrder> GetById(int Id);
        Task<List<PushesOrder>> GetAll();
    }
}
