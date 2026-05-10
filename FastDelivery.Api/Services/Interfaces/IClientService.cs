
using FastDelivery.Api.DTOs.Client;
using FastDelivery.Api.DTOs.Order;

namespace FastDelivery.Api.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientWithOrdersDto>> GetAllClientsAsync();

        Task<ClientWithOrdersDto?> GetClientByIdAsync(int id);

        Task<ClientResponseDto> CreateClientAsync(CreateClientDto dto);

        Task<ClientResponseDto?> UpdateClientAsync(UpdateClientDto dto, int id);

        Task<bool> DeleteClientAsync(int id);
    }
}
