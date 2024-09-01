using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogitem")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{    
    private readonly IItemService _catalogItemService;

    public CatalogItemController(       
        IItemService catalogItemService)
    {        
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItem(CreateItemRequest request)
    {
        var result = await _catalogItemService.AddItemAsync(request);

        return Ok(result);
    }

    [HttpGet]
    public async Task<GetCatalogByIdResponse> GetItemById(int id)
    {
        return await _catalogItemService.GetItemByIdAsync(id);
    }

    [HttpDelete]
    public async Task<DeleteResponse> DeleteItem(int id)
    {
        return await _catalogItemService.DeleteItemAsync(id);
    }

    [HttpPut]
    public async Task<UpdataResponse<ItemDto>> UpdateTypeAsync(ItemDto catalogItemDto)
    {
        return await _catalogItemService.UpdateItemAsync(catalogItemDto);
    }
}