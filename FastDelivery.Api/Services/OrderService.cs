using FastDelivery.Api.Data;
using FastDelivery.Api.DTOs.Client;
using FastDelivery.Api.DTOs.Order;
using FastDelivery.Api.DTOs.User;
using FastDelivery.Api.Models;
using FastDelivery.Api.Repositories;
using FastDelivery.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastDelivery.Api.Services
{
    public class OrderService : IOrdersService
    {
        private readonly AppDbContext _context;
        private readonly IMongoRepository _mongoRepository;

        public OrderService(AppDbContext context, IMongoRepository mongoRepository)
        {
            _context = context;
            _mongoRepository = mongoRepository;
        }
        //Crear Orden
        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var client = await _context.Clients.FindAsync(dto.ClientId);
            if (client == null)
                throw new Exception("El Cliente No Existe");

            var driver = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.DriverEmail);
            if (driver == null)
            {
                throw new Exception("El Repartidor No Existe");
            }
            var order = new Order
            {
                OrderNumber = dto.OrderNumber,
                Status = dto.Status,
                ClientId = dto.ClientId,
                DriverId = driver.Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderResponseDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Client = new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Address = order.Client.Address,
                    Phone = order.Client.Phone
                },
                Driver = driver != null ? new UserDto
                {
                    Id = driver.Id,
                    Name = driver.Name,
                    Email = driver.Email
                } : null,
                Message = "Order Creada Correctamente"
            };
        }
        //Obtener ordenes de un repartidor en especifico
        public async Task<List<OrderDetailsDto>> GetOrdersByDriverAsync(Guid id)
        {
            return await _context.Orders
             .Where(o => o.DriverId == id)
             .Select(o => new OrderDetailsDto
             {
                 Id = o.Id,
                 OrderNumber = o.OrderNumber,
                 Status = o.Status,

                 Client = new ClientDto
                 {
                     Id = o.Client.Id,
                     Name = o.Client.Name,
                     Address = o.Client.Address,
                     Phone = o.Client.Phone
                 },
                 Driver = new UserDto
                 {
                     Id = o.Driver.Id,
                     Name = o.Driver.Name,
                     Email =  o.Driver.Email
                 }
             })
             .ToListAsync();
        }

        //Obtener una sola Orden con sus Clientes y su Driver
        public async Task<OrderDetailsDto?> GetOrderById(int id)
        {
            return await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderDetailsDto
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    Status = o.Status,

                    Client = new ClientDto
                    {
                        Id = o.Client.Id,
                        Name = o.Client.Name,
                        Address = o.Client.Address,
                        Phone = o.Client.Phone
                    },
                    Driver = new UserDto
                    {
                        Id = o.Driver.Id,
                        Name = o.Driver.Name,
                        Email = o.Driver.Email
                    }
                })
                .FirstOrDefaultAsync();
        }
        //Editar Orden
        public async Task<OrderDetailsDto?> UpdateOrder( UpdateOrderDto dto, int id)
        {
            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Driver)
                .FirstOrDefaultAsync(o => o.Id == id);
            var driver = await _context.Users.FindAsync(dto.DriverId);

            if (order == null || driver == null)
                return null;
    
            order.Status = dto.Status;
            order.DriverId = dto.DriverId;

            await _context.SaveChangesAsync();

            return new OrderDetailsDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,

                Client = new ClientDto
                {
                    Id = order.Client.Id,
                    Name = order.Client.Name,
                    Address = order.Client.Address,
                    Phone = order.Client.Phone
                },

                Driver = new UserDto
                {
                    Id = order.Driver.Id,
                    Name = order.Driver.Name,
                    Email = order.Driver.Email
                }
            };
        }
        //Cambiar status de una orden
        public async Task<OrderResponseDto?> UpdateStatus( UpdateOrderStatusDto dto, int id)
        {
            var order = await _context.Orders
                   .Include(o => o.Client)
                   .Include(o => o.Driver)
                   .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return null;
            string previousStatus = order.Status;
            order.Status = dto.Status;

            await _context.SaveChangesAsync();

            var log = new OrderLog
            {
                OrderId = id,
                PreviousStatus = previousStatus,
                NewStatus = dto.Status,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Timestamp = DateTime.UtcNow
            };

            await _mongoRepository.CreateLogAsync(log);

            return new OrderResponseDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                Message = "Estatus de la Orden Actualizada",
                 Client = new ClientDto
                 {
                     Id = order.Client.Id,
                     Name = order.Client.Name,
                     Address = order.Client.Address,
                     Phone = order.Client.Phone
                 },
                Driver = new UserDto
                {
                    Id = order.Driver.Id,
                    Name = order.Driver.Name,
                    Email = order.Driver.Email
                } 
            };
        }
        public async Task<OrderHistoryResponseDto?> GetOrderHistory(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return null;

            var logs = await _mongoRepository.GetLogsByOrderIdAsync(id);

            var historyLogs = logs.Select(log => new OrderLogDto
            {
                PreviousStatus = log.PreviousStatus,
                NewStatus = log.NewStatus,
                Latitude = log.Latitude,
                Longitude = log.Longitude,
                Timestamp = log.Timestamp
            }).ToList();

            return new OrderHistoryResponseDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CurrentStatus = order.Status,
                History = historyLogs
            };
        }

        //Eliminar Orden
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
