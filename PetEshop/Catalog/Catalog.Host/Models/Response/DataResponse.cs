
namespace Catalog.Host.Models.Response
{
    public class DataResponse<T> : BaseResponse
    {
        public IEnumerable<T>? Data { get; set; }

        public override ResponceCode? RespCode => Data is null ? ResponceCode.Failed : ResponceCode.Seccusfull;
    }
}
