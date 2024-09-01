using Catalog.Host.Models.Response;
using Catalog.Host.Services.Abstractions;
using Catalog.Host.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Identity;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder(OrderDto order)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!;                       
            if(order != null && userId != null) 
            {
                var id = await _orderService.AddOrderAsync(userId, order.OrderItems);
                return Ok(id);
            }
            var response = new BaseResponse()
            {
                ErrorMessage = "order is null"
            };
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderById(int? orderId)
        {
            if(orderId != null && orderId != 0)
            {
                var order = await _orderService.GetOrderByIdAsync((int)orderId);
                return Ok(order);
            }

            var response = new BaseResponse()
            {
                ErrorMessage = "Id is Null"
            };
            return Ok(response);                     
        }

        [HttpPost]        
        [ProducesResponseType(typeof(DataResponse<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserId(string userId)
        {
            //var userIds = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value; 
            if(userId != null)
            {
                var id = await _orderService.GetOrderByUserIdAsync(userId!);
                return Ok(id);
            }

            var response = new BaseResponse()
            { 
                ErrorMessage = "userId null" 
            };

            return Ok(response);                       
        }
    }
}
