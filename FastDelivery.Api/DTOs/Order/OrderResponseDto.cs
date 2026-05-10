using FastDelivery.Api.DTOs.Client;
using FastDelivery.Api.DTOs.User;

namespace FastDelivery.Api.DTOs.Order
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public string Status { get; set; }

        public ClientDto Client { get; set; }

        public UserDto? Driver { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Message { get; set; }

    }
}
