using AutoMapper;
using Product.Entity;

namespace ProductWebApi.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDto, Product.Entity.Product>();
    }
}