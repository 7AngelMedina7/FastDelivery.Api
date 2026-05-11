using System.ComponentModel.DataAnnotations;

namespace FastDelivery.Api.DTOs.Client
{
    public class CreateClientDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public string Phone { get; set; }
    }
}
