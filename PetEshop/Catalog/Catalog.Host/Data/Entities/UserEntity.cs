namespace Catalog.Host.Data.Entities
{
    public class UserEntity
    {
        public string Id { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public ICollection<OrderEntity> Orders { get; set;} = new List<OrderEntity>();
    }
}
