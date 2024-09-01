using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Abstractions
{
    public interface IOrderService
    {
        Task<AddResponse<int>> AddOrderAsync(string UserId, ICollection<OrderItemDto> orderItem);
        Task<OrderResponse> GetOrderByIdAsync(int idOrder);        
        Task<DataResponse<OrderDto>> GetOrderByUserIdAsync(string userId);
    }
}
