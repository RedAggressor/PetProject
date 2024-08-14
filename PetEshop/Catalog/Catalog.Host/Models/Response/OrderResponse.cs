using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Response
{
    public class OrderResponse : BaseResponse
    {
        public OrderDto Order { get; set; } = null!;
        public override ResponceCode? RespCode => Order is null ? ResponceCode.Failed : ResponceCode.Seccusfull;
    }
}
