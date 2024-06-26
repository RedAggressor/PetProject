using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItemEntity, CatalogItemDto>()
            .ForMember(nameof(CatalogItemDto.PictureUrl), opt
                => opt.MapFrom<CatalogItemPictureResolver,
                string>(c => c.PictureFileName))
            .ReverseMap();
        
        CreateMap<CatalogTypeEntity, CatalogTypeDto>().ReverseMap();
    }
}