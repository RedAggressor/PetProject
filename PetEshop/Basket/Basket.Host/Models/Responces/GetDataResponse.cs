
namespace Basket.Host.Models.Responces
{
    public class GetDataResponse<T> : BaseResponse
    {
        public ICollection<T>? Data { get; set; } 
    }
}
