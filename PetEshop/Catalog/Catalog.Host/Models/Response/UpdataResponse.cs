namespace Catalog.Host.Models.Response
{
    public class UpdataResponse<T> : BaseResponse
    {
        public T? UpdataModel { get; set; }
    }
}
