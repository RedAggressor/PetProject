using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Abstractions
{
    public interface IOrderService
    {
        Task<AddResponse<string>> AddOrderAsync(string UserId);
        Task<AddResponse<string>> AddItemToOrder(string orderId, ICollection<AddOrderItemDto> orderItem);
        Task<OrderResponse> GetOrderByIdAsync(string idOrder);        
        Task<DataResponse<OrderDto>> GetOrderByUserIdAsync(string userId);
        Task<BaseResponse> UpdateOrderSatusAync(string orderId, string orderStatus);
    }
}
