namespace Catalog.Host.Data.Entities;

public class ItemEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string PictureFileName { get; set; } = null!;

    public int TypeId { get; set; }

    public TypeEntity Type { get; set; } = null!;

    public int AvailableStock { get; set; }
    public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
}
