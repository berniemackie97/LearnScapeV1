using AutoMapper;
using Core.BusinessModels;
using LearnScapeAPI.DTO;
using LearnScapeCore.BusinessModels.identity;

namespace LearnScapeAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductBM, ProductToReturnDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
