using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.enums;
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
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        if (request is null)
        {
            _logger.LogWarning("request null!");
            var responce = new BaseResponce()
            {
                ErrorMessage = "request null!"
            };
            responce.GetResponce();

            return Ok(responce);
        }
        var result = await _catalogService.GetByPageAsync(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(int? id)
    {
        if (id is null)
        {
            var responce = new BaseResponce()
            {
                ErrorMessage = "id is null"
            };
            responce.GetResponce();
            return Ok(responce);
        }
        var result = await _catalogItemService.GetCatalogItemsByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ListResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(int? idType)
    {
        if (idType is null)
        {
            var responce = new BaseResponce()
            {
                ErrorMessage = "id is null"
            };
            responce.GetResponce();
            return Ok(responce);
        }
        var result = await _catalogItemService.GetCatalogItemByTypeAsync(idType);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListType()
    {
        var result = await _catalogTypeService.GetList();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListItem()
    {
        var result = await _catalogService.GetCatalogItem();
        return Ok(result);
    }
}