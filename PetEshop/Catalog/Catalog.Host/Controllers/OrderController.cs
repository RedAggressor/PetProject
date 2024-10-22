using Catalog.Host.Models.Response;
using Catalog.Host.Services.Abstractions;
using Catalog.Host.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Identity;
using Infrastructure.Attributes;
using Catalog.Host.Models.Requests;

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
        [ProducesResponseType(typeof(AddResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!;                       
            var id = await _orderService.AddOrderAsync(userId);
            return Ok(id);            
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(AddResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItemToOrder(AddOrderRequest order)
        {
            var id = await _orderService.AddItemToOrder(order.OrderId, order.OrderItems);

            return Ok(id);
        }

        [HttpPost]        
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            return Ok(order);                                
        }

        [HttpPost]        
        [ProducesResponseType(typeof(DataResponse<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value; 
            var order = await _orderService.GetOrderByUserIdAsync(userId!);
            return Ok(order);        
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateStatusOrder(UpdateOrderStatusDto updateOrder)
        {
            var result = await _orderService.UpdateOrderSatusAync(updateOrder.OrderId, updateOrder.OrderStatus);
            return Ok(result);
        }
    }
}
