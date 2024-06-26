using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<AddResponse> Add(string name, string description, decimal price, int availableStock, int catalogTypeId, string pictureFileName);
    Task<CatalogItemDto> GetCatalogItemsByIdAsync(int id);
    Task<DataResponse<CatalogItemDto>> GetCatalogItemByTypeAsync(int idType);
    Task<DeleteResponse> DeleteAsync(int id);
    Task<CatalogItemDto> UpdateAsync(CatalogItemDto catalogItemDto);
}