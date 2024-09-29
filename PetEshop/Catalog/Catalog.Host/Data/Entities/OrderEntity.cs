using Catalog.Host.Models.enums;

namespace Catalog.Host.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public StatusType Status { get; set; } = StatusType.Empty;
        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();        
    }
}
