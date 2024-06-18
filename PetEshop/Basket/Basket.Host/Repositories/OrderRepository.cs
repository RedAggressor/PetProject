using Basket.Host.Data;
using Basket.Host.Data.Entities;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responces;
using Basket.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Basket.Host.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderRepository(IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }
        public async Task<int> AddOrder(OrderEntity orderEntity)
        {
            var entity = await _dbContext.Orders.AddAsync(orderEntity);

            return entity.Entity.Id;
        }

        public async Task<OrderEntity> GetOrder(int id)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

            return entity;
        }
    }
}
