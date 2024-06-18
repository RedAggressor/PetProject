using Basket.Host.Models.Requests;

namespace Basket.Host.Models.Responces
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<ItemRequest>? Items {  get; set; }        
        public int IdUser { get; set; }
        
    }
}
