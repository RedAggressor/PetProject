using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions;

public interface IItemRepository
{
    Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(int pageIndex, int pageSize, int typeFilter);
    Task<int> AddItemAsync(ItemEntity itemEntity);
    Task<ItemEntity> GetItemByIdAsync(int idItem);
    Task<ICollection<ItemEntity>> GetItemsByTypeIdAsync(int idType);
    Task<string> DeleteItemAsync(int id);
    Task<ItemEntity> UpdateItemAsync(ItemEntity catalogItem);
    Task<ICollection<ItemEntity>> GetCatalogItemsListAsync();
}