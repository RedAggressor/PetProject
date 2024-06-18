using Basket.Host.Data.Entities;
using Basket.Host.Models.Dto;
using Basket.Host.Repositories.Abstractions;
using Basket.Host.Services.Abstractions;

namespace Basket.Host.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> MakeOrder(OrderDto order)
        {
            // add mapper 

            var orderEntity = new OrderEntity()
            {
                UserId = order.IdUser,                
                Items = order.Items.Select(s => new ItemEntity()
                {
                    Id = s.Id,
                    Name = s.Name,
                    AvailableStock = s.AvailableStock,
                    BrandId = s.BrandId,
                    TypeId = s.TypeId,
                    Description = s.Description,
                    PictureUrl = s.PictureUrl,
                    Price = s.Price,
                }).ToList(),
            };

            return await _orderRepository.AddOrder(orderEntity);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int idOrder)
        {
            var entityt = await _orderRepository.GetOrder(idOrder);

            return new OrderDto()
            {
                Id = entityt.Id,
                IdUser = entityt.UserId,
                Items = entityt.Items.Select(s=>new ItemDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    AvailableStock = s.AvailableStock,
                    BrandId = s.BrandId,
                    TypeId = s.TypeId,
                    Description = s.Description,
                    PictureUrl = s.PictureUrl,
                    Price = s.Price,
                }).ToList(),
            };
        }
    }
}
