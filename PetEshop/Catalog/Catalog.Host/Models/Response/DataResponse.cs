namespace Catalog.Host.Models.Response
{
    public class DataResponse<T> : BaseResponce
    {
        public IEnumerable<T>? Data { get; set; }
        public override ResponceCode GetResponce() => Data is null ? ResponceCode.Failed: ResponceCode.Seccusfull;
    }
}
