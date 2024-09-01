using Catalog.Host.Configurations;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class ItemPictureResolver : IMemberValueResolver<ItemEntity, ItemDto, string, object>
{
    private readonly CatalogConfig _config;

    public ItemPictureResolver(IOptionsSnapshot<CatalogConfig> config)
    {
        _config = config.Value;
    }

    public object Resolve(
        ItemEntity source,
        ItemDto destination,
        string sourceMember,
        object destMember,
        ResolutionContext context)
    {
        return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
    }
}