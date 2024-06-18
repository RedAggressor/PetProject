using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : BaseService, ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        return await ExecutSafeAsync(async () =>
        {
            var filters = new Dictionary<CatalogTypeFilter, int>();

            if (brand.HasValue)
            {
                filters.Add(CatalogTypeFilter.Brand, brand.Value);
            }

            if (type.HasValue)
            {
                filters.Add(CatalogTypeFilter.Type, type.Value);
            }

            var result = await _httpClient
            .SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>(
               $"{_settings.Value.CatalogUrl}/items",
               HttpMethod.Post,
               new PaginatedItemsRequest<CatalogTypeFilter>()
               {
                   PageIndex = page,
                   PageSize = take,
                   Filters = filters
               });

            return result;
        });
    }   

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        var dtoType = await ExecutSafeAsync(async () =>
        await _httpClient
        .SendAsync<ListModel<CatalogType>, object>(
            $"{_settings.Value.CatalogUrl}/GetListType",
            HttpMethod.Post,
            null!)
        );

        var list = dtoType.List.Select(b => new SelectListItem
        {
            Value = $"{b.Id}",
            Text = $"{b.Type}"
        });

        return list;
    }
}
