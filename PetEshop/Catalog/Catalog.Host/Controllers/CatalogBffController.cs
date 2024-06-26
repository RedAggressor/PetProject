using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
//[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogTypeService _catalogTypeService;    
    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        ICatalogItemService catalogItemService,        
        ICatalogTypeService catalogTypeService
        )
    {
        _logger = logger;
        _catalogService = catalogService;
        _catalogItemService = catalogItemService;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    //[AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        if (request is not null)
        {
            var result = await _catalogService.GetByPageAsync(request.PageSize, request.PageIndex, request.FilterTypeId);
            return Ok(result);
        }        

        _logger.LogWarning("request null!");
        var responce = new BaseResponce()
        {
            ErrorMessage = "request null!"
        };
        responce.GetResponce();

        return Ok(responce);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItemById(int? id)
    {
        if (id is not null && id != default(int))
        {
            var result = await _catalogItemService.GetCatalogItemsByIdAsync((int)id);
            return Ok(result);
            
        }
        var responce = new BaseResponce()
        {
            ErrorMessage = "id is null"
        };
        responce.GetResponce();
        return Ok(responce);

    }

    [HttpPost]
    [ProducesResponseType(typeof(DataResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(int? idType)
    {
        if (idType is not null)
        {
            var result = await _catalogItemService.GetCatalogItemByTypeAsync((int)idType);
            return Ok(result);
        }        

        var responce = new BaseResponce()
        {
            ErrorMessage = "id is null"
        };
        responce.GetResponce();
        return Ok(responce);
    }

    [HttpPost]
    //[AllowAnonymous]
    [ProducesResponseType(typeof(DataResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListType()
    {
        var result = await _catalogTypeService.GetList();
        return Ok(result);
    }

    [HttpPost]
    //[AllowAnonymous]
    [ProducesResponseType(typeof(DataResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListItem()
    {
        var result = await _catalogService.GetCatalogItem();
        return Ok(result);
    }
}