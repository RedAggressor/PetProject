using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int typeFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

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

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(
        string name,
        string description,
        decimal price,
        int availableStock,        
        int catalogTypeId,
        string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {            
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem?> GetCatalogItemsByIdAsync(int? idItem)
    {
       return await _dbContext.CatalogItems
            .Include(i => i.CatalogType)            
            .FirstOrDefaultAsync(item => item.Id == idItem);
    }


    public async Task<ICollection<CatalogItem>> GetCatalogItemsByTypeAsync(int? idType)
    {
        return await _dbContext.CatalogItems            
            .Include(i => i.CatalogType)
            .Select(item => item)
            .Where(item => item.CatalogType.Id == idType)
            .ToListAsync();
    }

    public async Task<string> DeleteAsync(int? id)
    {
        if(id is null)
        {
            return "Id can`t be null";
        }
        var item = await GetCatalogItemsByIdAsync(id);
        var message = _dbContext.CatalogItems.Remove(item!);
        await _dbContext.SaveChangesAsync();
        return message.State.ToString();
    }

    public async Task<CatalogItem> Update(CatalogItem catalogItem)
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
    }

    public async Task<ICollection<CatalogItem>> GetCatalogItemList()
    {
        return await _dbContext.CatalogItems            
            .Include(i => i.CatalogType)
            .Select(item => item)            
            .ToListAsync();
    }
}