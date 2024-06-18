using Basket.Host.Models.Dto;

namespace Basket.Host.Services.Abstractions
{
    public interface IOrderService
    {
        Task<int> MakeOrder(OrderDto order);
        Task<OrderDto> GetOrderByIdAsync(int idOrder);
    }
}
