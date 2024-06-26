using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface ICatalogTypeRepository
    {
        Task<CatalogTypeEntity> GetById(int id);
        Task<ICollection<CatalogTypeEntity>> GetList();
        Task<int> AddTypeAsync(string type);
        Task<string> DeleteType(int id);
        Task<CatalogTypeEntity> Update(CatalogTypeEntity catalogType);
    }
}
