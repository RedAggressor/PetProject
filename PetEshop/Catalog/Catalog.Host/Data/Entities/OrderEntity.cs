namespace Catalog.Host.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();        
    }
}
