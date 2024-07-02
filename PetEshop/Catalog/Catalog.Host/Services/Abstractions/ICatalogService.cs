using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<ItemDto>> GetByPageAsync(int pageSize, int pageIndex, int filterTypeId);
    Task<DataResponse<ItemDto>> GetCatalogItem();
}