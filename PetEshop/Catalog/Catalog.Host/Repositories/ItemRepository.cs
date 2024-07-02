using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : BaseRepository, IItemRepository
{
    private readonly ApplicationDbContext _dbContext;   

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
        : base(logger)
    {
        _dbContext = dbContextWrapper.DbContext;       
    }

    public async Task<PaginatedItems<ItemEntity>> GetByPageAsync(
        int pageIndex,
        int pageSize,
        int typeFilter)
    {
        return await ExecutSafeAsync(async () =>
        {
            IQueryable<ItemEntity> query = _dbContext.Items;

            if (typeFilter > 0)
            {
                query = query.Where(w => w.TypeId == typeFilter);
            }

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Name)
               .Include(i => i.Type)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();

            return new PaginatedItems<ItemEntity>() 
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
            var item = await _dbContext.AddAsync(new ItemEntity
            {
                TypeId = catalogTypeId,
                Description = description,
                Name = name,
                PictureFileName = pictureFileName,
                Price = price
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        });
    }

    public async Task<ItemEntity> GetCatalogItemsByIdAsync(int idItem)
    {
        return await ExecutSafeAsync(async () =>
        {
            var entity = await _dbContext.Items
            .Include(i => i.Type)
            .FirstOrDefaultAsync(item => item.Id == idItem);

            return entity!;
        });
    }


    public async Task<ICollection<ItemEntity>> GetCatalogItemsByTypeAsync(int idType)
    {
        return await ExecutSafeAsync(async () =>
        {
            return await _dbContext.Items
            .Include(i => i.Type)
            .Select(item => item)
            .Where(item => item.Type.Id == idType)
            .ToListAsync();
        });
    }

    public async Task<string> DeleteAsync(int id)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await GetCatalogItemsByIdAsync(id);

            var message = _dbContext.Items.Remove(item!);

            await _dbContext.SaveChangesAsync();

            return message.State.ToString();
        });
    }

    public async Task<ItemEntity> Update(ItemEntity catalogItem)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await GetCatalogItemsByIdAsync(catalogItem.Id);

            item!.Price = catalogItem.Price;
            item.Description = catalogItem.Description;
            item.PictureFileName = catalogItem.PictureFileName;
            item.Name = catalogItem.Name;
            item.AvailableStock = catalogItem.AvailableStock;
            item.Type = catalogItem.Type;
            item.TypeId = catalogItem.TypeId;

            await _dbContext.SaveChangesAsync();

            return item;
        });
    }

    public async Task<ICollection<ItemEntity>> GetCatalogItemList()
    {
        return await ExecutSafeAsync(async () =>
        {
            var listEntity = await _dbContext.Items
            .Include(i => i.Type)
            .Select(item => item)
            .ToListAsync();

            return listEntity;
        });
    }
}