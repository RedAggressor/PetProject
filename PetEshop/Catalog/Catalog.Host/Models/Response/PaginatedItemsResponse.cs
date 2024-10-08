namespace Catalog.Host.Models.Response;

public class PaginatedItemsResponse<T> : BaseResponse
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public long Count { get; init; }

    public ICollection<T>? Data { get; init; }     
}
