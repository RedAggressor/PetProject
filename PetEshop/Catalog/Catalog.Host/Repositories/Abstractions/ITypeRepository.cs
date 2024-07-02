using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface ITypeRepository
    {
        Task<TypeEntity> GetById(int id);
        Task<ICollection<TypeEntity>> GetList();
        Task<int> AddTypeAsync(string type);
        Task<string> DeleteType(int id);
        Task<TypeEntity> Update(TypeEntity catalogType);
    }
}
