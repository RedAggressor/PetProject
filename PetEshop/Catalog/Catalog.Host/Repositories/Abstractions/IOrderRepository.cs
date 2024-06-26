using Catalog.Host.Data.Entities;

namespace Basket.Host.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<int> AddOrderAsync(int UserId, List<OrderCatalogItemEntity> orderItemList);
        Task<OrderEntity> GetOrderByIdAsync(int id);
        Task<ICollection<OrderEntity>> GetOrderByUserIdAsync(int UserId);
    }
};
