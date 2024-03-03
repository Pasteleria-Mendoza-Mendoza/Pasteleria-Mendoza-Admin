using Microsoft.EntityFrameworkCore;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.DAL
{
	public class PushesOrderDAL : IPushesOrder
	{
		readonly PADbContext dbContext;
		public PushesOrderDAL(PADbContext context)
		{
			dbContext = context;
		}

        public async Task ActualizarStock(int productoId, int cantidad)
        {
            try
            {
                // Obtener el producto del almacén general por su ID
                var producto = await dbContext.Products
                    .SingleOrDefaultAsync(p => p.IdProduct == productoId);

                if (producto != null)
                {
                    // Validar si hay suficientes existencias antes de decrementar
                    if (producto.Quantity >= cantidad)
                    {
                        // Decrementar la cantidad en el inventario del almacén general
                        for (int i = 0; i < cantidad; i++)
                        {
                            producto.Quantity--;
                        }
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // Manejar el caso en el que no hay suficientes existencias
                        throw new Exception($"No hay suficientes existencias del producto con ID {productoId} en el almacén general");
                    }
                }
                else
                {
                    // Manejar el caso en el que el producto no está en el inventario del almacén general
                    throw new Exception($"El producto con ID {productoId} no está en el inventario del almacén general");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al actualizar el inventario del almacén general: {ex.Message}");
                throw;
            }
        }

        public async Task AutorizarOrden(int ordenId)
        {
            var pedido = await dbContext.PushesOrder.FindAsync(ordenId);
            if (pedido != null)
            {            
                pedido.State = (byte)PushesOrder.StateOrder.Autorizado;
                await dbContext.SaveChangesAsync();           
            }
        }

        public void Create(PushesOrder pOrder)
        {
            try
            {
                // Agregar la orden a la tabla de solicitudes de orden
                dbContext.PushesOrder.Add(pOrder);

                // Guardar los cambios en la base de datos
                dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al crear la orden: {ex.Message}");
                throw;
            }
        }



        public async Task DeleteOrden(int ordenId)
        {
            var EliminarOrden = await dbContext.PushesOrder.FindAsync(ordenId);

            if (EliminarOrden != null)
            {
                dbContext.PushesOrder.Remove(EliminarOrden);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<PushesOrder> GetById(int id)
        {
            PushesOrder order = await dbContext.PushesOrder.FindAsync(id);
            return order;
        }

        public Task RechazarOrdenAsync(int ordenId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PushesOrder>> Search()
        {
            IQueryable<PushesOrder> query = dbContext.PushesOrder.AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<List<PushesOrder>> SearchByDate(DateTime date)
        {
            IQueryable<PushesOrder> query = dbContext.PushesOrder.Where(o => o.ReservationDate.Date == date.Date);
            return await query.ToListAsync();
        }
    }
}
