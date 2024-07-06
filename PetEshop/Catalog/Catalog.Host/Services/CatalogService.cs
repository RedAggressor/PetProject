using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly IItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;    

    public CatalogService(        
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {       
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<ItemDto>> GetByPageAsync(
        int pageSize, 
        int pageIndex,
        int filterTypeId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var corectionPage = 1; // page start at 1 not 0!

            pageIndex -= corectionPage;

            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize, filterTypeId);

            pageIndex += corectionPage;

            var responce = new PaginatedItemsResponse<ItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<ItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize                 
            };

            return responce;
        });
    }

    public async Task<DataResponse<ItemDto>> GetCatalogItem()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var list = await _catalogItemRepository.GetCatalogItemList();

            return new DataResponse<ItemDto>()
            {
                Data = list.Select(s => _mapper.Map<ItemDto>(s)).ToList()
            };
        });
    }
}