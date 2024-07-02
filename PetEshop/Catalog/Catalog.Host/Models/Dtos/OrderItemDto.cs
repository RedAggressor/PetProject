namespace Catalog.Host.Models.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public ItemDto Item { get; set; } = null!;
    }
}
