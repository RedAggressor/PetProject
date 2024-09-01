using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class TypeService : BaseDataService<ApplicationDbContext>, ITypeService
    {
        private readonly ITypeRepository _repository;
        private readonly IMapper _mapping;
        
        public TypeService(
            ITypeRepository repository,
            IMapper mapper,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<TypeService> logger)
            : base(dbContextWrapper, logger)
        {
            _repository = repository;
            _mapping = mapper;            
        }

        public async Task<AddResponse<int>> AddType(string type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var responce = await _repository.AddTypeAsync(type);

                return new AddResponse<int>()
                {
                    Id = responce
                };
            });
        }       

        public async Task<DeleteResponse> DeleteType(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var message = await _repository.DeleteType(id);

                return new DeleteResponse()
                {
                    Status = message
                };
            });
        }

        public async Task<UpdataResponse<TypeDto>> UpdateType(TypeDto typeDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var entity = _mapping.Map<TypeEntity>(typeDto);

                var upentity = await _repository.Update(entity);

                var dto = _mapping.Map<TypeDto>(upentity);

                return new UpdataResponse<TypeDto>()
                {
                    UpdataModel = dto
                };
            });
        }
    }
}
