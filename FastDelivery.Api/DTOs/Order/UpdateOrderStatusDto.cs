namespace FastDelivery.Api.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        public string Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
