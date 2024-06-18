using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<CatalogType?> GetById(int? id);        
        Task<int?> AddTypeAsync(string? type);
        Task<string?> DeleteType(int? id);
        Task<CatalogType> Update(CatalogType catalogType);
        Task<ICollection<CatalogType>> GetList();
    }
}
