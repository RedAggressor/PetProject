using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Response
{
    public class OrderResponse : BaseResponce
    {
        public int Id { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
