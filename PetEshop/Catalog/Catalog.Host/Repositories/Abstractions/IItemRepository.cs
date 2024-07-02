using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions;

public interface IItemRepository
{
    Task<PaginatedItems<ItemEntity>> GetByPageAsync(int pageIndex, int pageSize, int typeFilter);
    Task<int> Add(string name, string description, decimal price, int availableStock, int catalogTypeId, string pictureFileName);
    Task<ItemEntity> GetCatalogItemsByIdAsync(int id);
    Task<ICollection<ItemEntity>> GetCatalogItemsByTypeAsync(int idType);
    Task<string> DeleteAsync(int id);
    Task<ItemEntity> Update(ItemEntity catalogItem);
    Task<ICollection<ItemEntity>> GetCatalogItemList();
}