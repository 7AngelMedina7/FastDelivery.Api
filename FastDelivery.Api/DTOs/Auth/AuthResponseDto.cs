namespace FastDelivery.Api.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Message { get; set; }

    }
}
