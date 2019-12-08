using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Product;

namespace Api.Domain.Interfaces.Services.Product
{
  public interface IProductService
  {
    Task<ProductDto> GetById(Guid id);
    Task<IEnumerable<ProductDto>> GetAll();
    Task<ProductDtoCreateResult> Post(ProductDtoCreate product);
    Task<ProductDtoUpdateResult> Put(ProductDtoUpdate product);
    Task<bool> Delete(Guid id);
  }
}
