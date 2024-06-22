using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetByPageAsync(int pageSize, int pageIndex, int filtersTypeId);
    Task<IEnumerable<CatalogItemDto>> GetCatalogItem();
}