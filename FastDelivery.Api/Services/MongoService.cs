using FastDelivery.Api.Config;
using FastDelivery.Api.Models;
using FastDelivery.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FastDelivery.Api.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<OrderLog> _orderLogs;

        public MongoService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _orderLogs = database.GetCollection<OrderLog>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateLogAsync(OrderLog log)
        {
            await _orderLogs.InsertOneAsync(log);
        }

        public async Task<List<OrderLog>> GetLogsByOrderIdAsync(int orderId)
        {
            return await _orderLogs.Find(x => x.OrderId == orderId).ToListAsync();
        }
    }
}
