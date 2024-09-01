using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class ItemService : BaseDataService<ApplicationDbContext>, IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public ItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IItemRepository ItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _itemRepository = ItemRepository;
        _mapper = mapper;
    }

    public Task<AddResponse<int>> AddItemAsync(CreateItemRequest itemDto)
    {
        return ExecuteSafeAsync(async () =>
        {            
            var entity = _mapper.Map<ItemEntity>(itemDto);
            var id = await _itemRepository.AddItemAsync(entity);

            return new AddResponse<int>()
            {
                Id = id
            };
        });
    }

    public async Task<DeleteResponse> DeleteItemAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var message = await _itemRepository.DeleteItemAsync(id);
            return new DeleteResponse()
            {
                Status = message
            };
        });
    }

    public async Task<UpdataResponse<ItemDto>> UpdateItemAsync(ItemDto catalogItemDto)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = _mapper.Map<ItemEntity>(catalogItemDto);

            item = await _itemRepository.UpdateItemAsync(item);

            return new UpdataResponse<ItemDto>()
            {
                UpdataModel = _mapper.Map<ItemDto>(item)
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