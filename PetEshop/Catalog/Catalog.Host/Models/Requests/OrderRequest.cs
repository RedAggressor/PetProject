using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Requests
{
    public class OrderRequest
    {        
        public ICollection<OrderItemDto> items { get; set; } = new List<OrderItemDto>();
    }
}
