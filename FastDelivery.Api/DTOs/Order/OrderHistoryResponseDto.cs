namespace FastDelivery.Api.DTOs.Order
{
    public class OrderHistoryResponseDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string CurrentStatus { get; set; } = null!;
        public List<OrderLogDto> History { get; set; } = new();
    }
}
