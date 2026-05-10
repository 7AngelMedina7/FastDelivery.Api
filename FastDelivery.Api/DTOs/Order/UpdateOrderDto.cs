using FastDelivery.Api.DTOs.User;

namespace FastDelivery.Api.DTOs.Order
{
    public class UpdateOrderDto
    {
        public string Status { get; set; }

        public Guid DriverId { get; set; }
    }
}
