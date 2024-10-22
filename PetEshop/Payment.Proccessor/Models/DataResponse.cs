namespace Payment.Proccessor.Models
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
