using ProyectAdmin.BL.DTOs.OrdersDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.DAL;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;
using static ProyectAdmin.EN.PushesOrder;

namespace ProyectAdmin.BL
{
    public class PushesOrderBL : IPushesOrderBL
    {
        readonly IPushesOrder _pushesOrderDAL;
        readonly IUnitOfWork _unitOfWork;
        readonly IProduct _productDAL;

        public PushesOrderBL(IPushesOrder pushesOrderDAL, IUnitOfWork unitOfWork, IProduct product)
        {
            _pushesOrderDAL = pushesOrderDAL;
            _unitOfWork = unitOfWork;
            _productDAL = product;
        }

        public async Task<CreateOrderInputDTO> AddOrder(CreateOrderInputDTO orden)
        {
            try
            {
                PushesOrder newOrder = new PushesOrder()
                {
                    ReservationDate = DateTime.Now,
                    IdProduct = orden.IdProduct,
                    Names = orden.Names,
                    LastNames = orden.LastNames,
                    DUI = orden.DUI,
                    Phone = orden.Phone,
                    Amount = orden.Amount,
                    Adress = orden.Adress,
                    Dimension = orden.Dimension,
                    DeliverDate = orden.DeliverDate,
                    Dedication = orden.Dedication,
                    Details = orden.Details,
                    State = (byte)StateOrder.Pendiente,
                    Cost = orden.Cost
                };

                // Llama al método en tu DAL para agregar la nueva orden
                _pushesOrderDAL.Create(newOrder);

                // No es necesario llamar a SaveChanges aquí, ya que el método Create en tu DAL se encarga de guardar los cambios

                return orden;
            }
            catch (Exception err)
            {
                // Maneja la excepción según tus necesidades
                Console.WriteLine($"Error en AddOrder: {err.Message}");
                throw;
            }
        }


        public async Task AutorizarPedidoAsync(int ordenId)
        {
            try
            {
                // Obtener el pedido por ID
                var pedido = await _pushesOrderDAL.GetById(ordenId);

                // Validar si el pedido existe
                if (pedido == null)
                {
                    throw new ArgumentException($"No se encontró un pedido con ID {ordenId}");
                }

                // Verificar si el pedido ya ha sido autorizado
                if (pedido.State != (byte)PushesOrder.StateOrder.Pendiente)
                {
                    throw new InvalidOperationException($"El pedido con ID {ordenId} ya ha sido autorizado o está en un estado no válido para autorización.");
                }
                else
                {
                    // Obtener el producto asociado al pedido
                    var producto = await _productDAL.GetOne(pedido.IdProduct);

                    // Restar la cantidad del producto en el inventario
                    producto.Quantity -= pedido.Amount;

                    // Actualizar el producto en la capa de datos
                     _productDAL.Update(producto);

                    // Actualizar el estado del pedido
                    pedido.State = (byte)PushesOrder.StateOrder.Autorizado;

                    // Actualizar el pedido en la capa de datos
                    await _pushesOrderDAL.AutorizarOrden(ordenId);

                    // Guardar los cambios en la base de datos
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades
                Console.WriteLine($"Error al autorizar el pedido: {ex.Message}");
                throw;
            }
        }


        public async Task DeleteOrden(int ordeinId)
        {
            try
            {
                await _pushesOrderDAL.DeleteOrden(ordeinId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la compra: {ex.Message}");
            }
        }

        public async Task<List<GetAllOrderOutputDTO>> GetAllOrder(DateTime? specificDate = null)
        {
            List<PushesOrder> orders;

            if (specificDate.HasValue)
            {
                orders = await _pushesOrderDAL.SearchByDate(specificDate.Value);
            }
            else
            {
                orders = await _pushesOrderDAL.Search();
            }

            List<GetAllOrderOutputDTO> order = orders.Select(p => new GetAllOrderOutputDTO()
            {
                ReservationDate = DateTime.Now,
                IdProduct = p.IdProduct,
                Names = p.Names,
                LastNames = p.LastNames,
                DUI = p.DUI,
                Phone = p.Phone,
                Amount = p.Amount,
                Adress = p.Adress,
                Dimension = p.Dimension,
                DeliverDate = DateTime.Now,
                Dedication = p.Dedication,
                Details = p.Details,
                State = StateOrder.Pendiente,
                Cost = p.Cost
            }).ToList();

            return order;
        }

        public async Task<GetAllOrderOutputDTO> GetOrderById(int Id)
        {
            PushesOrder isOrden = await _pushesOrderDAL.GetById(Id);
            if (isOrden == null)
                throw new Exception($"Order by id:{Id} is not exits");
            GetAllOrderOutputDTO order = new GetAllOrderOutputDTO()
            {
                ReservationDate = DateTime.Now,
                IdProduct = isOrden.IdProduct,
                Names = isOrden.Names,
                LastNames = isOrden.LastNames,
                DUI = isOrden.DUI,
                Phone = isOrden.Phone,
                Amount = isOrden.Amount,
                Adress = isOrden.Adress,
                Dimension = isOrden.Dimension,
                DeliverDate = DateTime.Now,
                Dedication = isOrden.Dedication,
                Details = isOrden.Details,
                State = StateOrder.Pendiente,
                Cost = isOrden.Cost
            };
            return order;
        }

        public async Task RechazarPedidoAsync(int ordenId)
        {
            try
            {
                // Obtener el pedido por ID
                var pedido = await _pushesOrderDAL.GetById(ordenId);

                // Validar si el pedido existe
                if (pedido == null)
                {
                    throw new ArgumentException($"No se encontró un pedido con ID {ordenId}");
                }

                // Verificar si el pedido ya ha sido autorizado o rechazado
                if (pedido.State != (byte)PushesOrder.StateOrder.Pendiente)
                {
                    throw new InvalidOperationException($"El pedido con ID {ordenId} ya ha sido autorizado o rechazado.");
                }

                // Cambiar el estado del pedido a "Rechazado"
                pedido.State = (byte)PushesOrder.StateOrder.Rechazado;

                // Guardar los cambios en la base de datos
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades
                Console.WriteLine($"Error al rechazar el pedido: {ex.Message}");
                throw;
            }
        }
    }
}
