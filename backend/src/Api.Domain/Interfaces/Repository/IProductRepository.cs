using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repository
{
  public interface IProductRepository : IRepository<ProductEntity>
  {
    Task<ProductEntity> FindByName(string name);
  }
}
