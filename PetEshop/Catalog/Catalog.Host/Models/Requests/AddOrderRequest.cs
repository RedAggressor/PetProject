using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Requests
{
    public class AddOrderRequest
    {
        public string OrderId { get; set; } = null!;
        public ICollection<AddOrderItemDto> OrderItems { get; set; } = new List<AddOrderItemDto>();
    }
}
