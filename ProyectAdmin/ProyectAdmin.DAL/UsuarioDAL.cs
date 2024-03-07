using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectAdmin.DAL
{
    internal class UsuarioDAL : IUsuarioDAL
    {
        	readonly PADbContext dbContext;
		public UsuarioDAL(PADbContext context)
		{
			dbContext = context;
		}
        public void Create(Usuarios pUsuarios)
        {
            dbContext.Add(pUsuarios);
        }

        public void Delete(Usuarios pUsuarios)
        {
            dbContext.Remove(pUsuarios);
        }

        public async Task<List<Usuarios>> GetAll()
        {
            var list = await dbContext.Usuarios.ToListAsync();
            return list;
        }

        public async Task<Usuarios> GetById(int Id)
        {
            Usuarios? usuarios = await dbContext.Usuarios.FirstOrDefaultAsync(s => s.UsuarioId == Id);
            if (usuarios != null)
                return usuarios;
            else
                return new Usuarios();
        }

        public async Task<List<Usuarios>> Search(Usuarios pUsuarios)
        {
            var query = dbContext.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pUsuarios.Nombre))
                query = query.Where(s => s.Nombre == pUsuarios.Nombre);

            // Otras condiciones de búsqueda...

            query = query.OrderByDescending(s => s.UsuarioId).AsQueryable();

            query = query.Include(s => s.Rol).AsQueryable();

            return await query.ToListAsync();
        }


        public void Update(Usuarios pUsuarios)
        {
            dbContext.Update(pUsuarios);
        }
    }
}

