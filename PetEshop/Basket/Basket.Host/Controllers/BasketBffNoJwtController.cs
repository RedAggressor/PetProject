using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Services.Abstractions;
using Infrastructure.Attributes;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]    
    public class BasketBffNoJwtController : ControllerBase
    {
        private IBasketService _basketService;
        private ICookieService _cookieService;

        public BasketBffNoJwtController(IBasketService basketService, ICookieService cookieService)
        {
            _basketService = basketService;
            _cookieService = cookieService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItems(AddDataRequest addData)
        {
            var userId = await _cookieService.GetUniqueIdFromCookieAsync(HttpContext);

            var result = await _basketService.AddItemsAsync(userId, addData.Items);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetDataResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItems()
        {
            var userId = await _cookieService.GetUniqueIdFromCookieAsync(HttpContext);

            var result = await _basketService.GetItemsAsync(userId);

            return Ok(result);
        }
    }
}
