using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Data.Entities
{
    public class BrandEntity
    {
        [Key]
        public int Id { get; set; }

        public string Brand { get; set; } = null!;
    }
}
