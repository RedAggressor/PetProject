using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<int> AddOrderAsync(int UserId, List<OrderItemEntity> orderItemList);
        Task<OrderEntity> GetOrderByIdAsync(int id);
        Task<ICollection<OrderEntity>> GetOrderByUserIdAsync(int UserId);
    }
};
