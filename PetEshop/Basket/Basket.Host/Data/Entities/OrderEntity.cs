namespace Basket.Host.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public ICollection<ItemEntity> Items { get; set; } = new List<ItemEntity>();        
        public int UserId { get; set; }
    }
}
