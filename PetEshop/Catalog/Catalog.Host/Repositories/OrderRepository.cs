﻿using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public OrderRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<OrderRepository> logger)
            : base (logger)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddOrderAsync(int UserId, List<OrderItemEntity> orderItemList)
        {
            return await ExecutSafeAsync(async () =>
            {
                var order = await _dbContext.Orders
                    .AddAsync(new OrderEntity()
                    {
                        UserId = UserId
                    });

                await _dbContext.OrderItems.AddRangeAsync(orderItemList.Select(s => new OrderItemEntity()
                {
                    Count = s.Count,
                    OrderId = order.Entity.Id,
                    ItemId = s.ItemId
                }));

                await _dbContext.SaveChangesAsync();

                return order.Entity.Id!;
            });
        }

        public async Task<OrderEntity> GetOrderByIdAsync(int id)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext
                .Orders
                .Include(i => i.OrderItems)                
                .ThenInclude(i =>i.Item)
                .FirstOrDefaultAsync(o => o.Id == id);

                return entity!;
            });
        }

        public async Task<ICollection<OrderEntity>> GetOrderByUserIdAsync(int UserId)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext
                .Orders
                .Include(i => i.OrderItems)
                .ThenInclude(i => i.Item)
                .Where(w => w.UserId == UserId)
                .ToListAsync();

                return entity!;
            });
        }
    }
}
