using FastDelivery.Api.Models;

namespace FastDelivery.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task CreateAsync(User user);
    }
}