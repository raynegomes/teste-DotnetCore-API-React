using System;
using System.Net.Mime;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Application
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDepenciesRepository(services);

      var config = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new DtoToModelProfile());
        cfg.AddProfile(new EntityToDtoProfile());
        cfg.AddProfile(new ModelToEntityProfile());
      });

      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);

      services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "API de produtos - Teste DTI",
        Version = "v1",
        Description = "API criada para teste prático sênior da empresa DTI",
        Contact = new OpenApiContact
        {
          Name = "Rayne Gomes",
          Email = string.Empty,
          Url = new Uri("https://github.com/raynegomes")
        }
      }
      ));

      services.AddCors(options =>
        {
          options.AddDefaultPolicy(
            builder =>
            {
              builder.WithOrigins("http://localhost:3000").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
      //services.AddControllers();
      services.AddControllers()
      .ConfigureApiBehaviorOptions(options =>
      {
        options.InvalidModelStateResponseFactory = context =>
        {
          var result = new BadRequestObjectResult(context.ModelState);

          // TODO: add `using using System.Net.Mime;` to resolve MediaTypeNames
          result.ContentTypes.Add(MediaTypeNames.Application.Json);
          result.ContentTypes.Add(MediaTypeNames.Application.Xml);

          return result;
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de produtos - Teste DPI");
      });

      //Redireciona o link da rota pricipal para o swegger
      var option = new RewriteOptions();
      option.AddRedirect("ˆ$^", "swagger");
      app.UseRewriter(option);


      app.UseRouting();

      app.UseAuthorization();

      app.UseCors(builder =>
      {
        builder.WithOrigins("http://localhost:3000").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
