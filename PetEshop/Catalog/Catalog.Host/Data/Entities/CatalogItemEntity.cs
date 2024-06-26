namespace Catalog.Host.Data.Entities;

public class CatalogItemEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string PictureFileName { get; set; } = null!;

    public int CatalogTypeId { get; set; }

    public CatalogTypeEntity CatalogType { get; set; } = null!;

    public int AvailableStock { get; set; }
    public ICollection<OrderCatalogItemEntity> OrderItems { get; set; } = new List<OrderCatalogItemEntity>();
}
