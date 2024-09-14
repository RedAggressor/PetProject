namespace Basket.Host.Models.Requests
{
    public class AddDataRequest
    {
        public ICollection<ItemDto> Items { get; set; } = null!;
    }
}
