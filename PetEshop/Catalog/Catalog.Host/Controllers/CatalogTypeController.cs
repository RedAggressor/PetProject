using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ITypeService _serivice;

    public CatalogTypeController(ITypeService service)
    {       
        _serivice = service;
    }    

    [HttpPost]
    public async Task<AddResponse<int>> AddType(string type)
    {
        return await _serivice.AddType(type);        
    }

    [HttpPut]
    public async Task<UpdataResponse<TypeDto>> UpdateType(TypeDto catalogType)
    {
        return await _serivice.UpdateType(catalogType);
    }

    [HttpDelete]
    public async Task<DeleteResponse> DeleteType(int? id)
    {
        if(id != null && id != 0)
        {
            return await _serivice.DeleteType((int)id);
        }

        return new DeleteResponse() 
        { 
            ErrorMessage = " id is null"
        };
       
    }
}