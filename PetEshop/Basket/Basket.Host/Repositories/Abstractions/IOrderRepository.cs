using Basket.Host.Data.Entities;

namespace Basket.Host.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<int> AddOrder(OrderEntity orderEntity);
        Task<OrderEntity> GetOrder(int id);
    }
};
