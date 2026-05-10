using FastDelivery.Api.Models;

namespace FastDelivery.Api.Services.Interfaces
{
    public interface IMongoRepository
    {
        Task CreateLogAsync(OrderLog log);
        Task<List<OrderLog>> GetLogsByOrderIdAsync(int orderId);
    }
}
