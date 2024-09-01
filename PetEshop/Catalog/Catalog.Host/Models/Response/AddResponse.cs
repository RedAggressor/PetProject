namespace Catalog.Host.Models.Response
{
    public class AddResponse<T> : BaseResponse
    {
        public T Id { get; set; } = default(T)!;
    }
}
