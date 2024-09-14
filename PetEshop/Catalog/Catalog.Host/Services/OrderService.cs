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

        public async Task<AddResponse<string>> AddOrderAsync(string UserId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _orderRepository.AddOrderAsync(UserId);

                return new AddResponse<string>()
                {
                    Id = id.ToString()
                };
            });
        }

        public async Task<AddResponse<string>> AddItemToOrder(string orderId, ICollection<AddOrderItemDto> orderItem)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderItemEntity = new List<OrderItemEntity>();

                orderItem.ToList().ForEach(o => orderItemEntity.Add(new OrderItemEntity()
                {
                    ItemId = o.ItemId,
                    Count = o.Count,
                }));

                if (int.TryParse(orderId, out int resultId))
                {
                    var id = await _orderRepository.AddItemToOrderAsync(resultId, orderItemEntity);

                    return new AddResponse<string>()
                    {
                        Id = id.ToString(),
                    };
                }

                return new AddResponse<string>()
                {
                    ErrorMessage = "Id can`t conver to int"
                };
            });
        }

        public async Task<OrderResponse> GetOrderByIdAsync(string idOrder)
        {
            return await ExecuteSafeAsync((async () =>
            {
                if (int.TryParse(idOrder, out int resultId))
                {
                    var entity = await _orderRepository.GetOrderByIdAsync(resultId);

                    return new OrderResponse()
                    {
                        Order = new OrderDto()
                        {
                            Id = entity.Id,
                            OrderItems = entity.OrderItems.Select(o => new OrderItemDto()
                            {
                                Id = o.Id,
                                Count = o.Count                                
                            }).ToList()
                        }
                    };
                }

                return new OrderResponse()
                {
                    ErrorMessage = "orderId is not string or something go wrong!"
                };
            }));
        }

        public async Task<DataResponse<OrderDto>> GetOrderByUserIdAsync(string userId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var listEntity = await _orderRepository.GetOrderByUserIdAsync(userId);

                return new DataResponse<OrderDto>()
                {
                    Data = listEntity.Select(s => new OrderDto()
                    {
                        Id = s.Id,
                        OrderItems = s.OrderItems.Select(o => new OrderItemDto()
                        {
                            Id = o.Id,
                            Count = o.Count,
                            Item = _mapper.Map<ItemDto>(o.Item)

                        }).ToList()
                    }),
                };
            });
        }
    }
}
