using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ItemEntity, ItemDto>()
            .ForMember(nameof(ItemDto.PictureUrl), opt
                => opt.MapFrom<ItemPictureResolver,
                string>(c => c.PictureFileName))
            .ReverseMap();
        
        CreateMap<TypeEntity, TypeDto>().ReverseMap();
    }
}