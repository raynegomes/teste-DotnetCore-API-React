using Api.Domain.Interfaces.Services.Product;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
  public class ConfigureService
  {
    public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<IProductService, ProductService>();
    }
  }
}
