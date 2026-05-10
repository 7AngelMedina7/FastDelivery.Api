using FastDelivery.Api.DTOs.Order;

namespace FastDelivery.Api.DTOs.Client
{
    public class ClientWithOrdersDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}
