using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly IItemRepository _itemRepository;
    private readonly ITypeRepository _typrRepository;
    private readonly IMapper _mapper;    

    public CatalogService(        
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IItemRepository itemRepository,
        ITypeRepository typeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {       
        _itemRepository = itemRepository;
        _typrRepository = typeRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<ItemDto>> GetItemByPageAsync(PaginatedItemsRequest paginatedItems)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var corectionPage = 1; // page start at 1 not 0!

            paginatedItems.PageIndex -= corectionPage;

            var result = await _itemRepository.GetItemsByPageAsync(
                paginatedItems.PageIndex,
                paginatedItems.PageSize,
                paginatedItems.FilterTypeId);

            paginatedItems.PageIndex += corectionPage;

            var responce = new PaginatedItemsResponse<ItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<ItemDto>(s)).ToList(),
                PageIndex = paginatedItems.PageIndex,
                PageSize = paginatedItems.PageSize
            };

            return responce;
        });
    }

    public async Task<DataResponse<ItemDto>> GetItemsListAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var list = await _itemRepository.GetCatalogItemsListAsync();

            return new DataResponse<ItemDto>()
            {
                Data = list.Select(s => _mapper.Map<ItemDto>(s)).ToList()
            };
        });
    }

    public async Task<DataResponse<ItemDto>> GetItemByTypeAsync(int idType)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var items = await _itemRepository.GetItemsByTypeIdAsync(idType);

            return new DataResponse<ItemDto>()
            {
                Data = items.Select(i => _mapper.Map<ItemDto>(i))
            };
        });
    }

    public async Task<DataResponse<TypeDto>> GetTypesListAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var entity = await _typrRepository.GetTypesListAsync();

            var list = entity.Select(s => _mapper.Map<TypeDto>(s));

            return new DataResponse<TypeDto>()
            {
                Data = list
            };
        });
    }

    public async Task<GetCatalogByIdResponse> GetItemByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _itemRepository.GetItemByIdAsync(id);

            var dto = _mapper.Map<ItemDto>(item);

            return new GetCatalogByIdResponse()
            {
                ItemDto = dto
            };
        });
    }
}