namespace Catalog.Host.Models.Response
{
    public class AddResponse : BaseResponce
    {
        public int? Id { get; set; }
        public override ResponceCode GetResponce() =>
            Id is null ?
            ResponceCode.Failed :
            ResponceCode.Seccusfull;
    }
}
