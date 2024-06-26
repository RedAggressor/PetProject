using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Requests
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public ICollection<OrderItemDto> items { get; set; } = new List<OrderItemDto>();
    }
}
