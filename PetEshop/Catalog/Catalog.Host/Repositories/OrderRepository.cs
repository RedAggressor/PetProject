using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.enums;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<OrderRepository> logger)
            : base(logger)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddOrderAsync(string UserId)
        {
            return await ExecutSafeAsync(async () =>
            {
                var order = await GetExistEmptyOrderByUserIdAsync(UserId);

                if (order is null)
                {
                    var result = await _dbContext.Orders
                    .AddAsync(new OrderEntity()
                    {
                        UserId = UserId
                    });

                    order = result.Entity;
                } 

                await _dbContext.SaveChangesAsync();

                return order.Id!;
            });
        }

        public async Task<int> AddItemToOrderAsync(int orderId, List<OrderItemEntity> orderItemList)
        {
            await _dbContext.OrderItems.AddRangeAsync(orderItemList.Select(s => new OrderItemEntity()
            {
                Count = s.Count,
                OrderId = orderId,
                ItemId = s.ItemId
            }));

            var items = orderItemList.SelectMany(s =>
                _dbContext.Items
                    .Include(i => i.Type)
                    .Where(f => f.Id == s.ItemId)
                    .OrderByDescending(o => o.Id)).ToList();

            var orderitems = orderItemList.OrderByDescending(o => o.ItemId).ToList();

            for (int i = 0; i < orderItemList.Count; i++)
            {
                if ((items[i].AvailableStock - orderItemList[i].Count) < 0)
                {
                    throw new Exception("item availableStock has not anought value");
                }

                items[i].AvailableStock -= orderItemList[i].Count;
            };

            foreach (var item in items)
            {
                await _dbContext.SaveChangesAsync();
            };

            await UpdateOrderStatus(orderId, StatusType.Confirmed);

            return orderId;
        }

        public async Task<OrderEntity> GetOrderByIdAsync(int id)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext.Orders
                .Include(i => i.OrderItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(i => i.Type)
                .FirstOrDefaultAsync(o => o.Id == id);
                
                return entity!;
            });
        }

        public async Task<ICollection<OrderEntity>> GetOrderByUserIdAsync(string UserId)
        {
            return await ExecutSafeAsync(async () =>
            {
                var entity = await _dbContext
                .Orders
                .Include(i => i.OrderItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(i=>i.Type)
                .Where(w => w.UserId == UserId)
                .ToListAsync();

                return entity!;
            });
        }

        private async Task<StatusType> UpdateOrderStatus(int orderId, StatusType status)
        {

            var order = await _dbContext.Orders.FirstOrDefaultAsync(f => f.Id == orderId);

            order!.Status = status;

            await _dbContext.SaveChangesAsync();

            return status;

        }

        private async Task<OrderEntity?> GetExistEmptyOrderByUserIdAsync(string userId)
        {
            return await _dbContext.Orders
                .Where(f => f.UserId == userId)
                .FirstOrDefaultAsync(f => f.Status == StatusType.Empty);
        }
    }
}
