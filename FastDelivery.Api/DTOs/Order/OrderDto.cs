namespace FastDelivery.Api.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public string Status { get; set; }

        public string ClientName { get; set; }

        public string? DriverName { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
