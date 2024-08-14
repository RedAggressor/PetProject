using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface IItemService
{
    Task<AddResponse<int>> AddItemAsync(CreateItemRequest itemDto);
    Task<GetCatalogByIdResponse> GetItemByIdAsync(int id);    
    Task<DeleteResponse> DeleteItemAsync(int id);
    Task<UpdataResponse<ItemDto>> UpdateItemAsync(ItemDto catalogItemDto);
}