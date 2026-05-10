using FastDelivery.Api.Data;
using FastDelivery.Api.DTOs.Auth;
using FastDelivery.Api.DTOs.Client;
using FastDelivery.Api.DTOs.Order;
using FastDelivery.Api.Models;
using FastDelivery.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace FastDelivery.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }
        //Crear Cliente
        public async Task<ClientResponseDto> CreateClientAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                Name = dto.Name,
                Address =  dto.Address,
                Phone = dto.Phone,
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return new ClientResponseDto
            {
                Name = client.Name,
                Address = client.Address,
                Phone = client.Phone,
                Message = "Cliente Creado Correctamente"
            };
        }
        //Obtener Clientes con sus ordenes
        public async Task<List<ClientWithOrdersDto>> GetAllClientsAsync()
        {
            return await _context.Clients
                .Select(c => new ClientWithOrdersDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Phone = c.Phone,

                    Orders = c.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        OrderNumber = o.OrderNumber,
                        Status = o.Status,
                        CreatedAt = o.CreatedAt
                    }).ToList()
                })
                .ToListAsync();
        }
        //Obtener un solo cliente con sus ordenes
        public async Task<ClientWithOrdersDto?> GetClientByIdAsync(int id)
        {
            return await _context.Clients
                .Where(c => c.Id == id)
                .Select(c => new ClientWithOrdersDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Phone = c.Phone,

                    Orders = c.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        OrderNumber = o.OrderNumber,
                        Status = o.Status,
                        CreatedAt = o.CreatedAt
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
        //Editar Cliente
        public async Task<ClientResponseDto?> UpdateClientAsync(UpdateClientDto dto, int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return null;

            client.Name = dto.Name;
            client.Address = dto.Address;
            client.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return new ClientResponseDto
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                Phone = client.Phone,
                Message = "Cliente Actualizado Correctamente"
            };
        }
        //Eliminar Cliente
        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
