namespace Catalog.Host.Models.Response
{
    public class ListResponse<T> : BaseResponce
    {
        public IEnumerable<T>? List { get; set; }
        public override ResponceCode GetResponce() => List is null ? ResponceCode.Failed: ResponceCode.Seccusfull;
    }
}
