
using System.ComponentModel.DataAnnotations;

namespace FastDelivery.Api.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; } = "Driver";
    }
}
