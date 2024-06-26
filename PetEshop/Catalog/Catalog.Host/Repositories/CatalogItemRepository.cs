using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : BaseRepository, ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;   

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
        : base(logger)
    {
        _dbContext = dbContextWrapper.DbContext;       
    }

    public async Task<PaginatedItems<CatalogItemEntity>> GetByPageAsync(
        int pageIndex,
        int pageSize,
        int typeFilter)
    {
        return await ExecutSafeAsync(async () =>
        {
            IQueryable<CatalogItemEntity> query = _dbContext.CatalogItems;

            if (typeFilter > 0)
            {
                query = query.Where(w => w.CatalogTypeId == typeFilter);
            }

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Name)
               .Include(i => i.CatalogType)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();

            return new PaginatedItems<CatalogItemEntity>() 
            { 
                TotalCount = totalItems,
                Data = itemsOnPage 
            };
        });
    }

    public async Task<int> Add(
        string name,
        string description,
        decimal price,
        int availableStock,        
        int catalogTypeId,
        string pictureFileName)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await _dbContext.AddAsync(new CatalogItemEntity
            {
                CatalogTypeId = catalogTypeId,
                Description = description,
                Name = name,
                PictureFileName = pictureFileName,
                Price = price
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        });
    }

    public async Task<CatalogItemEntity> GetCatalogItemsByIdAsync(int idItem)
    {
        return await ExecutSafeAsync(async () =>
        {
            var entity = await _dbContext.CatalogItems
            .Include(i => i.CatalogType)
            .FirstOrDefaultAsync(item => item.Id == idItem);

            return entity!;
        });
    }


    public async Task<ICollection<CatalogItemEntity>> GetCatalogItemsByTypeAsync(int idType)
    {
        return await ExecutSafeAsync(async () =>
        {
            return await _dbContext.CatalogItems
            .Include(i => i.CatalogType)
            .Select(item => item)
            .Where(item => item.CatalogType.Id == idType)
            .ToListAsync();
        });
    }

    public async Task<string> DeleteAsync(int id)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await GetCatalogItemsByIdAsync(id);

            var message = _dbContext.CatalogItems.Remove(item!);

            await _dbContext.SaveChangesAsync();

            return message.State.ToString();
        });
    }

    public async Task<CatalogItemEntity> Update(CatalogItemEntity catalogItem)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await GetCatalogItemsByIdAsync(catalogItem.Id);

            item!.Price = catalogItem.Price;
            item.Description = catalogItem.Description;
            item.PictureFileName = catalogItem.PictureFileName;
            item.Name = catalogItem.Name;
            item.AvailableStock = catalogItem.AvailableStock;
            item.CatalogType = catalogItem.CatalogType;
            item.CatalogTypeId = catalogItem.CatalogTypeId;

            await _dbContext.SaveChangesAsync();

            return item;
        });
    }

    public async Task<ICollection<CatalogItemEntity>> GetCatalogItemList()
    {
        return await ExecutSafeAsync(async () =>
        {
            var listEntity = await _dbContext.CatalogItems
            .Include(i => i.CatalogType)
            .Select(item => item)
            .ToListAsync();

            return listEntity;
        });
    }
}