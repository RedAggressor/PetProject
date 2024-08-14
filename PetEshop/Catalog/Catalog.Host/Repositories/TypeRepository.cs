using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories
{
    public class TypeRepository : BaseRepository, ITypeRepository
    {
        private readonly ApplicationDbContext _dbContext;       

        public TypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<TypeRepository> logger)
            : base (logger)
        {
            _dbContext = dbContextWrapper.DbContext;           
        }

        public async Task<TypeEntity> GetById(int id)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.Types
                .FirstOrDefaultAsync(f => f.Id == id);

                return entity!;
            });
        }

        public async Task<ICollection<TypeEntity>> GetTypesListAsync()
        {
            return await ExecutSafeAsync(async () =>
            {
                return await _dbContext.Types.ToListAsync();
            });
        }

        public async Task<int> AddTypeAsync(string type)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.Types.AddAsync(new TypeEntity()
                {
                    Type = type
                });

                await _dbContext.SaveChangesAsync();

                return entity.Entity.Id!;
            });
        }

        public async Task<string> DeleteType(int id)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await GetById(id);

                var message = _dbContext.Types.Remove(entity!);

                await _dbContext.SaveChangesAsync();

                return message.State.ToString();
            });
        }

        public async Task<TypeEntity> Update(TypeEntity catalogType)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await GetById(catalogType.Id);

                entity!.Type = catalogType.Type;

                await _dbContext.SaveChangesAsync();

                return entity!;
            });
        }
    }
}
