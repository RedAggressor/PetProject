namespace Catalog.Host.Models.Dtos;

    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

