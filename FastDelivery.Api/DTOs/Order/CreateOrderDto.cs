namespace FastDelivery.Api.DTOs.Order
{
    public class CreateOrderDto
    {
        public string OrderNumber { get; set; }

        public int ClientId { get; set; }

        public string DriverEmail { get; set; }

        public string Status { get; set; } = "Pendiente";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
