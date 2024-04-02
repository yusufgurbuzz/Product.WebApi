using AutoMapper;
using Product.Entity;

namespace ProductWebApi.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateProductDto, Product.Entity.Product>();
        CreateMap<Product.Entity.Product, ProductDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductStock, opt => opt.MapFrom(src => src.ProductStock))
            .ReverseMap();
        CreateMap<ProductInsertionDto,Product.Entity.Product>();
    }
}