using AutoMapper;
using Product.Entity;

namespace Draft.Product.WebAPI.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProductMaterial, ProductMaterialDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        });
        var mapper = conf.CreateMapper();
        
    }
}