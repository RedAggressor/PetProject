using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Services.Abstractions;

namespace Basket.Host.Controllers;

[ApiController]
//[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger; // release logger!! or wrapper do this
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetDataResponse<ItemRequest>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItems()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.GetItems(basketId!);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)] // responce model will be different
    public async Task<IActionResult> AddItems(ICollection<ItemRequest> items)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!;
        await _basketService.AddItems(basketId, items);
        return Ok();
    }
}
