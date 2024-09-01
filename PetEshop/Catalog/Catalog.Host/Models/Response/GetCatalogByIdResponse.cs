using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Response
{
    public class GetCatalogByIdResponse : BaseResponse
    {
        public ItemDto ItemDto { get; set; } = null!;
    }
}
