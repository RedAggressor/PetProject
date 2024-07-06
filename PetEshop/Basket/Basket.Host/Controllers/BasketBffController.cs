using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Services.Abstractions;
using Infrastructure.Identity;
using Infrastructure.Models;
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
    [ProducesResponseType(typeof(GetDataResponse<ItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItems(string userId)
    {
        //var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.GetItems(userId!);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItems(AddRequest request)
    {
        //var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!;
        await _basketService.AddItems(request.UserId, request.Item);
        return Ok(new BaseResponce());
    }
}
