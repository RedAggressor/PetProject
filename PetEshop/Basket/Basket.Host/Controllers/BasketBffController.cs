using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Services.Abstractions;
using Infrastructure.Attributes;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetDataResponse<ItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItems()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        var response = await _basketService.GetItemsAsync(userId!);

        return Ok(response);
    }

    [HttpPost]
    [ValidateRequestBody]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItems(AddDataRequest request)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!;

        var responce = await _basketService.AddItemsAsync(userId, request.Items);

        return Ok(responce);
    }
}
