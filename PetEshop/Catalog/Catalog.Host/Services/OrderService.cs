using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<OrderService> logger,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<AddResponse> AddOrderAsync(int UserId, List<OrderItemRequest> orderItem)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderItemEntity = new List<OrderItemEntity>();

                orderItem.ForEach(o => orderItemEntity.Add(new OrderItemEntity()
                {
                    ItemId = o.ItemId,
                    Count = o.Count,
                }));

                var id = await _orderRepository.AddOrderAsync(UserId, orderItemEntity);

                return new AddResponse()
                {
                    Id = id,
                };
            });
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int idOrder)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var entity = await _orderRepository.GetOrderByIdAsync(idOrder);

                return new OrderResponse()
                {
                    Id = entity.Id,
                    OrderItems = entity.OrderItems.Select(o => new OrderItemDto()
                    {
                        Id = o.Id,
                        Count = o.Count,
                        Item = _mapper.Map<ItemDto>(o.Item)
                    }).ToList()
                };
            });
        }

        public async Task<DataResponse<OrderResponse>> GetOrderByUserIdAsync(int userId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var listEntity = await _orderRepository.GetOrderByUserIdAsync(userId);

                return new DataResponse<OrderResponse>()
                {
                    Data = listEntity.Select(s => new OrderResponse()
                    {
                        Id = s.Id,
                        OrderItems = s.OrderItems.Select(o => new OrderItemDto()
                        {
                            Id = o.Id,
                            Count = o.Count,
                            Item = _mapper.Map<ItemDto>(o.Item)

                        }).ToList(),

                    }).ToList(),
                };
            });
        }
    }
}
