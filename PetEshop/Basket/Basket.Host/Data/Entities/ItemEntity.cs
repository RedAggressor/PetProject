using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.Host.Data.Entities
{
    [Table("Catalog")]
    public class ItemEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        [Column("CatalogTypeId")]
        public int TypeId { get; set; }                
        [Column("CatalogBrandId")]
        public int BrandId { get; set; }
        public int AvailableStock { get; set; }
        
    }
}
