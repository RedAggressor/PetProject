using Basket.Host.Data.Entities;

namespace Basket.Host.Models.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public int TypeId { get; set; }
        public int BrandId { get; set; }
        public int AvailableStock { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;
    }
}
