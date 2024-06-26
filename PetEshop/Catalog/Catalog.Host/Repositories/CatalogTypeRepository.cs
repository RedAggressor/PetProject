using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : BaseRepository, ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;       

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
            : base (logger)
        {
            _dbContext = dbContextWrapper.DbContext;           
        }

        public async Task<CatalogTypeEntity> GetById(int id)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.CatalogTypes
                .FirstOrDefaultAsync(f => f.Id == id);

                return entity!;
            });
        }

        public async Task<ICollection<CatalogTypeEntity>> GetList()
        {
            return await ExecutSafeAsync(async () =>
            {
                return await _dbContext.CatalogTypes.ToListAsync();
            });
        }

        public async Task<int> AddTypeAsync(string type)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.CatalogTypes.AddAsync(new CatalogTypeEntity()
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

                var message = _dbContext.CatalogTypes.Remove(entity!);

                await _dbContext.SaveChangesAsync();

                return message.State.ToString();
            });
        }

        public async Task<CatalogTypeEntity> Update(CatalogTypeEntity catalogType)
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
