using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
    internal class RolDAL: IRolDAL
    {
        readonly PADbContext dbContext;
        public RolDAL(PADbContext context)
        {
            dbContext = context;
        }
        public void Create(Rol pRol)
        {
            dbContext.Add(pRol);
        }
        public void Delete(Rol pRol)
        {
            dbContext.Remove(pRol);
        }


        public async Task<List<Rol>> Search(Rol pRol)
        {

            var query = dbContext.Rol.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                query = query.Where(r => r.Nombre == pRol.Nombre);
            if (pRol.RolId > 0)
                query = query.Where(s => s.RolId == pRol.RolId);

            return await query.ToListAsync();

        }



        public void Update(Rol pRol)
        {
            dbContext.Update(pRol);
        }

        public async Task<List<Rol>> GetAll()
        {

            var list = await dbContext.Rol.ToListAsync();
            return list;

        }

        public async Task<Rol> GetById(int Id)
        {
            Rol? rol = await dbContext.Rol.FirstOrDefaultAsync(r => r.RolId == Id);

            if (rol != null)
                return rol;
            else
                return new Rol();

        }
    }
}
