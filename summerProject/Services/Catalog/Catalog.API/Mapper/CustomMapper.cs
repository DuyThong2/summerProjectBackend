using AutoMapper;
using Catalog.API.Dtos.Ingredient;
using Catalog.API.Models;

namespace Catalog.API.Mapper
{
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile() {
            CreateMap<(PackageIngredient pi, Ingredient ing), IngredientInPackageDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ing.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ing.Name))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.ing.Unit))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.pi.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.pi.UnitPrice));

            CreateMap<IngredientAddDTO, Ingredient>().ReverseMap();

        }
    }
}
