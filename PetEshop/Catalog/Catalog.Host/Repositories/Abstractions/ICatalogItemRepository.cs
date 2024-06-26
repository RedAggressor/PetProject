using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItemEntity>> GetByPageAsync(int pageIndex, int pageSize, int typeFilter);
    Task<int> Add(string name, string description, decimal price, int availableStock, int catalogTypeId, string pictureFileName);
    Task<CatalogItemEntity> GetCatalogItemsByIdAsync(int id);
    Task<ICollection<CatalogItemEntity>> GetCatalogItemsByTypeAsync(int idType);
    Task<string> DeleteAsync(int id);
    Task<CatalogItemEntity> Update(CatalogItemEntity catalogItem);
    Task<ICollection<CatalogItemEntity>> GetCatalogItemList();
}