using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]

[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger; 
    private readonly ITypeService _serivice;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ITypeService service)
    {
        _logger = logger;
        _serivice = service;
    }    

    [HttpPost]
    public async Task<AddResponse> AddType(string? type)
    {
        if(type is null)
        {
            return new AddResponse() 
            {
                ErrorMessage = "Null data",
                RespCode = ResponceCode.Failed
            };
        }

        return await _serivice.AddType(type);
    }

    [HttpPut]
    public async Task<UpdataResponse<TypeDto>> UpdateType(TypeDto? catalogType)
    {
        if (catalogType is null)
        {
            return new UpdataResponse<TypeDto>() 
            {
                ErrorMessage = "Null data",
                RespCode = ResponceCode.Failed

            };
        }

        return await _serivice.UpdateType(catalogType);
    }

    [HttpDelete]
    public async Task<DeleteResponse> DeleteType(int? id)
    {
        if (id is null)
        {
            return new DeleteResponse() 
            { 
               
                RespCode = ResponceCode.Failed,
                ErrorMessage = "id is null"
            };
        }

        return await _serivice.DeleteType((int)id);
    }
}