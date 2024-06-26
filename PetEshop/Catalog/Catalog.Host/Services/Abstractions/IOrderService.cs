
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Abstractions
{
    public interface IOrderService
    {
        Task<AddResponse> AddOrderAsync(int UserId, List<OrderItemRequest> orderItem);
        Task<OrderResponse> GetOrderByIdAsync(int idOrder);
        Task<DataResponse<OrderResponse>> GetOrderByUserIdAsync(int userId);
    }
}
