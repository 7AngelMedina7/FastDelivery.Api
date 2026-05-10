namespace FastDelivery.Api.DTOs.Order
{
    public class OrderLogDto
    {
        public string PreviousStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
