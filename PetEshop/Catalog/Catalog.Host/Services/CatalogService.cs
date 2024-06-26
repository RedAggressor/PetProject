using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;    

    public CatalogService(        
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {       
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetByPageAsync(
        int pageSize, 
        int pageIndex,
        int filterTypeId)
    {
        return await ExecuteSafeAsync(async () =>
        {     
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize, filterTypeId);
            
            var responce = new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize                 
            };

            return responce;
        });
    }

    public async Task<DataResponse<CatalogItemDto>> GetCatalogItem()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var list = await _catalogItemRepository.GetCatalogItemList();

            return new DataResponse<CatalogItemDto>()
            {
                Data = list.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList()
            };
        });
    }
}