using Basket.Host.Models.Requests;

namespace Basket.Host.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public ICollection<ItemDto>? Items { get; set; }
        public int IdUser { get; set; }
    }
}
