using FastDelivery.Api.DTOs.Auth;
using FastDelivery.Api.DTOs.User;

namespace FastDelivery.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<UserDto> ProfileAsync(Guid id);

    }
}
