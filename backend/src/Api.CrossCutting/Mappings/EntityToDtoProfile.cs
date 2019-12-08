using Api.Domain.Dtos.Product;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
  public class EntityToDtoProfile : Profile
  {
    public EntityToDtoProfile()
    {
      CreateMap<ProductDto, ProductEntity>().ReverseMap();

      CreateMap<ProductDtoCreateResult, ProductEntity>().ReverseMap();

      CreateMap<ProductDtoUpdateResult, ProductEntity>().ReverseMap();
    }

  }
}
