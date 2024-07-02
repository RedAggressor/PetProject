namespace Catalog.Host.Data.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; } = null!;
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;                
    }
}
