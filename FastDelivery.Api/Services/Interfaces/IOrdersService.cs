
using FastDelivery.Api.DTOs.Order;

namespace FastDelivery.Api.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto);
        Task<List<OrderDetailsDto>> GetOrdersByDriverAsync(Guid id);
        Task<OrderDetailsDto?> GetOrderById(int id);
        Task<OrderDetailsDto?> UpdateOrder( UpdateOrderDto dto, int id);
        Task<OrderResponseDto?> UpdateStatus(UpdateOrderStatusDto dto,int id);
        Task<OrderHistoryResponseDto?> GetOrderHistory(int id);
        Task<bool> DeleteOrderAsync(int id);
    }
}
