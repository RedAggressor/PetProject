namespace Catalog.Host.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public UserEntity User { get; set; } = null!;       
        public int UserId { get; set; }
        public ICollection<OrderCatalogItemEntity> OrderItems { get; set; } = new List<OrderCatalogItemEntity>();        
    }
}
