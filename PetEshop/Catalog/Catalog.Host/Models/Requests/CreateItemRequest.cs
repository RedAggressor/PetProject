namespace Catalog.Host.Models.Requests;

public class CreateItemRequest
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string PictureFileName { get; set; } = null!;

    public int TypeId { get; set; }

    public int AvailableStock { get; set; }
}