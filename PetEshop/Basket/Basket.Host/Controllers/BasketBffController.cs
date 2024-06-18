using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Services.Abstractions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

[ApiController]
//[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
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
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> TestAdd(TestAddRequest data)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        await _basketService.TestAdd(basketId!, data.Data);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(TestGetResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TestGet()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.TestGet(basketId!);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddResponce<int>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItems(ICollection<ItemRequest> items)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        await _basketService.AddItems(basketId, items);
        return Ok();
    }

}
