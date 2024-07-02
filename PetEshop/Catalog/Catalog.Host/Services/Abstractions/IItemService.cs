using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface IItemService
{
    Task<AddResponse> Add(string name, string description, decimal price, int availableStock, int catalogTypeId, string pictureFileName);
    Task<ItemDto> GetCatalogItemsByIdAsync(int id);
    Task<DataResponse<ItemDto>> GetCatalogItemByTypeAsync(int idType);
    Task<DeleteResponse> DeleteAsync(int id);
    Task<ItemDto> UpdateAsync(ItemDto catalogItemDto);
}