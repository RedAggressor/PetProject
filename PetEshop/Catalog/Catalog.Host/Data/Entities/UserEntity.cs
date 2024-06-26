namespace Catalog.Host.Data.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Mail { get; set; } = null!;
        public ICollection<OrderEntity> Orders { get; set;} = new List<OrderEntity>();
    }
}
