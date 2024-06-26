using Basket.Host.Repositories.Abstractions;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(
            ILogger<BaseRepository> logger,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper
            ) : base(logger)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddUserAsync(UserEntity user)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.Users.AddAsync(user);

                return entity.Entity.Id!;
            });
        }

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                return entity!;
            });
        }
    }
}
