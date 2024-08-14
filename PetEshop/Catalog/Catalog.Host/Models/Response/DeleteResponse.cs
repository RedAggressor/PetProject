namespace Catalog.Host.Models.Response
{
    public class DeleteResponse : BaseResponse
    {
        public string? Status { get; set; }
        public override ResponceCode? RespCode => Status is null ? ResponceCode.Failed : ResponceCode.Seccusfull;

    }
}
