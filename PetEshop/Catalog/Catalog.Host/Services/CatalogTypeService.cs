using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _repository;
        private readonly IMapper _mapping;
        private readonly ILogger<CatalogTypeService> _logger;

        public CatalogTypeService(
            ICatalogTypeRepository repository,
            IMapper mapper,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeService> logger)
            : base(dbContextWrapper, logger)
        {
            _repository = repository;
            _mapping = mapper;
            _logger = logger;
        }

        public async Task<AddResponse> AddType(string? type)
        {
            return await ExecuteSafeAsync(async () =>
            {        
                var responce = await _repository.AddTypeAsync(type);

                return new AddResponse() 
                {
                    Id = responce.Value
                };
            });
        }

        public async Task<ListResponse<CatalogTypeDto>> GetList()
        {
            return await ExecuteSafeAsync(async () => 
            {
                var entity = await _repository.GetList();

                var list = entity.Select(s=>_mapping.Map<CatalogTypeDto>(s));

                return new ListResponse<CatalogTypeDto>()
                { 
                    List = list
                };
            });
        }

        public async Task<DeleteResponse> DeleteType(int? id)
        {
            return await ExecuteSafeAsync(async() =>
            {
                if(id is null)
                {
                    _logger.LogWarning("id null");
                    throw new BusinessException("id null");
                }

                var message = await _repository.DeleteType(id);

                return new DeleteResponse()
                {
                    Status = message
                };
            });
        }

        public async Task<UpdataResponse<CatalogTypeDto>> UpdateType(CatalogTypeDto typeDto)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var entity = _mapping.Map<CatalogType>(typeDto);
                var upentity = await _repository.Update(entity);
                var dto = _mapping.Map<CatalogTypeDto>(upentity);
                return new UpdataResponse<CatalogTypeDto>() 
                { 
                    UpdataModel = dto
                };
            });
        }
    }
}
