﻿using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<int> AddOrderAsync(string UserId);
        Task<int> AddItemToOrderAsync(int orderId, List<OrderItemEntity> orderItemList);
        Task<OrderEntity> GetOrderByIdAsync(int id);
        Task<ICollection<OrderEntity>> GetOrderByUserIdAsync(string UserId);
        Task<BaseResponse> updateOrderStatusAsync(int orderId, string statusOrder);
    }
};
