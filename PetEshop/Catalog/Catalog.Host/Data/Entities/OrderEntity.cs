namespace Catalog.Host.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }           
        public int UserId { get; set; }
        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();        
    }
}
