using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
  public class ConfigureRepository
  {
    public static void ConfigureDepenciesRepository(IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
      serviceCollection.AddScoped<IProductRepository, ProductImplementation>();
      serviceCollection.AddDbContext<MyContext>(
        options => options.UseMySql("Server=raynegomes.com.br;Port=3306;Database=testedti;Uid=dti;Pwd=dti2019#")
      );
    }
  }
}
