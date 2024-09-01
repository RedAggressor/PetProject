using Catalog.Host.Data.Entities;

namespace Basket.Host.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(UserEntity user);
        Task<UserEntity> GetUserByIdAsync(int userId);
    }
}
