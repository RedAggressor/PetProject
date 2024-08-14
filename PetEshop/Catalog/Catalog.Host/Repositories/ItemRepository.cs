using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories;

public class ItemRepository : BaseRepository, IItemRepository
{
    private readonly ApplicationDbContext _dbContext;   

    public ItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<ItemRepository> logger)
        : base(logger)
    {
        _dbContext = dbContextWrapper.DbContext;       
    }

    public async Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(
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

    public async Task<int> AddItemAsync(ItemEntity itemEntity)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await _dbContext.AddAsync(itemEntity);

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        });
    }

    public async Task<ItemEntity> GetItemByIdAsync(int idItem)
    {
        return await ExecutSafeAsync(async () =>
        {
            var entity = await _dbContext.Items
            .Include(i => i.Type)
            .FirstOrDefaultAsync(item => item.Id == idItem);

            return entity!;
        });
    }

    public async Task<ICollection<ItemEntity>> GetItemsByTypeIdAsync(int idType)
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

    public async Task<string> DeleteItemAsync(int id)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await GetItemByIdAsync(id);

            var message = _dbContext.Items.Remove(item!);

            await _dbContext.SaveChangesAsync();

            return message.State.ToString();
        });
    }

    public async Task<ItemEntity> UpdateItemAsync(ItemEntity catalogItem)
    {
        return await ExecutSafeAsync(async () =>
        {
            var item = await GetItemByIdAsync(catalogItem.Id);

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

    public async Task<ICollection<ItemEntity>> GetCatalogItemsListAsync()
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