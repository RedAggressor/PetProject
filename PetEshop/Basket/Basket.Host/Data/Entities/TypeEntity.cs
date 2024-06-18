using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Data.Entities
{
    public class TypeEntity
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; } = null!;
    }
}
