using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ITypeService
    {
        Task<UpdataResponse<TypeDto>> UpdateType(TypeDto typeDto);
        Task<DeleteResponse> DeleteType(int id);
        Task<AddResponse> AddType(string type);
        Task<DataResponse<TypeDto>> GetList();
    }
}
