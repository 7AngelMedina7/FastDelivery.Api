namespace FastDelivery.Api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public Guid DriverId { get; set; }

        public User Driver { get; set; }

        public string Status { get; set; }
        public string OrderNumber { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}