namespace FastDelivery.Api.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
            = new List<Order>();
    }
}