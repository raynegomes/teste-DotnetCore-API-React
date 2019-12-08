using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Product;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services.Product;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
  public class ProductService : IProductService
  {
    private IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }
    public async Task<bool> Delete(Guid id)
    {
      return await _repository.DeleteAsync(id);
    }

    public async Task<ProductDto> GetById(Guid id)
    {
      var entity = await _repository.SelectAsync(id);
      return _mapper.Map<ProductDto>(entity);
    }

    public async Task<IEnumerable<ProductDto>> GetAll()
    {
      var listEntity = await _repository.SelectAsync();
      return _mapper.Map<IEnumerable<ProductDto>>(listEntity);
    }

    public async Task<ProductDtoCreateResult> Post(ProductDtoCreate product)
    {
      var model = _mapper.Map<ProductModel>(product);
      var entity = _mapper.Map<ProductEntity>(model);

      var idExist = await _repository.ExistAsync(entity.Id);

      if (idExist)
      {
        entity.Id = Guid.NewGuid();
      }

      //Verifica se o nome existe em outro produto
      var nameExist = await _repository.FindByName(product.Name);

      if (nameExist != null)
      {
        throw new Exception("BadRequest: Nome já utilizado em outro produto");
      }

      var result = await _repository.InsertAsync(entity);

      return _mapper.Map<ProductDtoCreateResult>(result);
    }

    public async Task<ProductDtoUpdateResult> Put(ProductDtoUpdate product)
    {
      var model = _mapper.Map<ProductModel>(product);
      var entity = _mapper.Map<ProductEntity>(model);

      //Verifica se o nome existe em outro produto (caso seja alterado)
      var nameExist = await _repository.FindByName(product.Name);

      if (nameExist != null && !nameExist.Id.Equals(product.Id))
      {
        throw new Exception("BadRequest: Nome já utilizado em outro produto}");
      }

      var result = await _repository.UpdateAsync(entity);

      return _mapper.Map<ProductDtoUpdateResult>(result);
    }
  }
}
