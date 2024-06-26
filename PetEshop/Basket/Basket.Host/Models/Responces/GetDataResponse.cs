namespace Basket.Host.Models.Responces
{
    public class GetDataResponse<T>
    {
        public ICollection<T>? Data { get; set; } 
    }
}
