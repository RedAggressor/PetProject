using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder(List<OrderItemRequest> order)
        {
            var userIds = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            int userId;

            if (int.TryParse(userIds, out userId))
            {
                var id = await _orderService.AddOrderAsync(userId, order);

                return Ok(id);
            };

            return Ok(new BaseResponce()
            {
                ErrorMessage = "userId not valid!",
                RespCode = ResponceCode.Failed
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderById(int? orderId)
        {
            if (orderId is not null && orderId is not default(int))
            {
                var order = await _orderService.GetOrderByIdAsync((int)orderId);
                return Ok(order);
            }

            return Ok(new BaseResponce()
            {
                RespCode = ResponceCode.Failed,
                ErrorMessage = "Order id is null or 0"
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var userIds = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value; 

            int userId;

            if (int.TryParse(userIds, out userId))
            {
                var id = await _orderService.GetOrderByUserIdAsync(userId);

                return Ok(id);
            };

            return Ok(new BaseResponce()
            {
                ErrorMessage = "userId not valid!",
                RespCode = ResponceCode.Failed
            });
        }
    }
}
