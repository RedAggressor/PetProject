using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogbff")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService
        )
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<ItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByPage(PaginatedItemsRequest request)
    {
        if (request is null)
        {
            _logger.LogWarning("request null!");
            var responce = new BaseResponse()
            {
                ErrorMessage = "request null!"
            };

            return Ok(responce);
        }

        var result = await _catalogService.GetItemByPageAsync(request);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetCatalogByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItemById(int? id)
    {
        if (id != null && id != 0)
        {
            var response = await _catalogService.GetItemByIdAsync((int)id);
            return Ok(response);
           
        }

        var result = new BaseResponse()
        {
            ErrorMessage = "data is null"
        };

        return Ok(result);


    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItemByTypeId(int? idType)
    {
        if(idType != null && idType != 0) 
        {
            var result = await _catalogService.GetItemByTypeAsync((int)idType);
            return Ok(result);
        }

        var response = new BaseResponse()
        { 
            ErrorMessage = "id is null"
        };

        return Ok(response);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResponse<TypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListType()
    {
        var result = await _catalogService.GetTypesListAsync();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListItem()
    {
        var result = await _catalogService.GetItemsListAsync();
        return Ok(result);
    }
}