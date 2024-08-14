using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<ItemDto>> GetItemByPageAsync(PaginatedItemsRequest paginatedItems);
    Task<DataResponse<ItemDto>> GetItemsListAsync();
    Task<DataResponse<TypeDto>> GetTypesListAsync();
    Task<GetCatalogByIdResponse> GetItemByIdAsync(int id);
    Task<DataResponse<ItemDto>> GetItemByTypeAsync(int idType);
}