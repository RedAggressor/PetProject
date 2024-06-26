namespace Catalog.Host.Data.Entities
{
    public class OrderCatalogItemEntity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int CatalogItemId { get; set; }
        public CatalogItemEntity CatalogItem { get; set; } = null!;
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;                
    }
}
