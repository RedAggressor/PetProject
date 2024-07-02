using Infrastructure.Enums;

namespace Catalog.Host.Models.Response
{
    public class DeleteResponse : BaseResponce
    {
        public string? Status { get; set; }

        //public override ResponceCode GetResponce() =>   Status is null ?   ResponceCode.Failed :     ResponceCode.Seccusfull;

    }
}
