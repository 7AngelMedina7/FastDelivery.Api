using System.ComponentModel.DataAnnotations;

namespace FastDelivery.Api.DTOs.Order
{
    public class CreateOrderDto
    {
        [Required]
        public string OrderNumber { get; set; }
        [Required]
        public int ClientId { get; set; }

        public string DriverEmail { get; set; } = string.Empty;

        public string Status { get; set; } = "Pendiente";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
