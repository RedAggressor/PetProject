using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Requests
{
    public class OrderRequest
    {
        public string UserId { get; set; } = null!;
        public ICollection<OrderItemRequest> items { get; set; } = new List<OrderItemRequest>();
    }
}
