using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<CatalogType?> GetById(int? id)
        {
            return await _dbContext.CatalogTypes
                .FirstOrDefaultAsync(f => f.Id == id);           
        }

        public async Task<ICollection<CatalogType>> GetList()
        {
            return await _dbContext.CatalogTypes.ToListAsync();             
        }

        public async Task<int?> AddTypeAsync(string? type)
        {
            if(type is null)
            {
                return null;
            }

            var entity = await _dbContext.CatalogTypes.AddAsync(new CatalogType()
            { 
                Type = type
            });

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<string?> DeleteType(int? id)
        {
            var entity = await GetById(id);
            var message = _dbContext.CatalogTypes.Remove(entity!);

            await _dbContext.SaveChangesAsync();

            return message.State.ToString();
        }

        public async Task<CatalogType> Update(CatalogType catalogType)
        {
            var entity = await GetById(catalogType.Id);
            entity!.Type = catalogType.Type;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
