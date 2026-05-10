using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FastDelivery.Api.Models
{
    public class OrderLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int OrderId { get; set; }
        public string PreviousStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
