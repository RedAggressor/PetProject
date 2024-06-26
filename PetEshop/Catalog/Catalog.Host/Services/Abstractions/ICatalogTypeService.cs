using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<UpdataResponse<CatalogTypeDto>> UpdateType(CatalogTypeDto typeDto);
        Task<DeleteResponse> DeleteType(int id);
        Task<AddResponse> AddType(string type);
        Task<DataResponse<CatalogTypeDto>> GetList();
    }
}
