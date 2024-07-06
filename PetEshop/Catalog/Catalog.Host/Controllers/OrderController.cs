using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Abstractions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers
{
    [ApiController]
    //[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
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
        public async Task<IActionResult> AddOrder(OrderRequest order)
        {
            //var userIds = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;                       

            if (int.TryParse(order.UserId, out int userId))
            {
                var id = await _orderService.AddOrderAsync(userId, order.items);

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
        [ProducesResponseType(typeof(DataResponse<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserId(string userIds)
        {
            //var userIds = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value; 

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
