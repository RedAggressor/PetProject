using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, IItemService
{
    private readonly IItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public Task<AddResponse> Add(
        string name,
        string description,
        decimal price,
        int availableStock,        
        int catalogTypeId,
        string pictureFileName) // create dto responce model!!!
    {
        return ExecuteSafeAsync(async () => new AddResponse() 
        { 
            Id = await _catalogItemRepository.Add(
            name,
            description,
            price,
            availableStock,
            catalogTypeId,
            pictureFileName)
        });
    }

    public async Task<ItemDto> GetCatalogItemsByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogItemRepository.GetCatalogItemsByIdAsync(id);

            var dto = _mapper.Map<ItemDto>(item);

            return dto!;
        });
        
    }

    public async Task<DataResponse<ItemDto>> GetCatalogItemByTypeAsync(int idType)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var itemColections = new List<ItemDto>();

            var items = await _catalogItemRepository.GetCatalogItemsByTypeAsync(idType);

            itemColections.AddRange(items.Select(i => _mapper.Map<ItemDto>(i)));

            return new DataResponse<ItemDto>()
            {
                Data = itemColections
            };
        });
    }

    public async Task<DeleteResponse> DeleteAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var message = await _catalogItemRepository.DeleteAsync(id);
            return new DeleteResponse() 
            { 
                Status = message
            };
        });
    }

    public async Task<ItemDto> UpdateAsync(ItemDto catalogItemDto)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = _mapper.Map<ItemEntity>(catalogItemDto);

            item = await _catalogItemRepository.Update(item);

            var dto = _mapper.Map<ItemDto>(item);

            return dto!;
        });
    }

}