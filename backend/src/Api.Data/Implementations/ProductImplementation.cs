using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
  public class ProductImplementation : BaseRepository<ProductEntity>, IProductRepository
  {
    private DbSet<ProductEntity> _context;

    public ProductImplementation(MyContext context) : base(context)
    {
      _context = context.Set<ProductEntity>();
    }

    public async Task<ProductEntity> FindByName(string name)
    {
      return await _context.FirstOrDefaultAsync(p => p.Name.Equals(name));
    }
  }
}
