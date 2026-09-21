using Carter;
using Catalog.Service.Catalog.Application.Products;
using Catalog.Service.Catalog.Application.Products.Contracts;
using Catalog.Service.Catalog.Domain.Product;
using Catalog.Service.Catalog.Infrustructure.Persistence;
using Catalog.Service.Catalog.Infrustructure.Persistence.Repositories;
using Catalog.Service.Security;
using Catalog.Service.Security.Services.Options;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Service;

public static class DependencyInjection
{
     public static void AddEshopServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCarter();
 
  
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddCors();
        services.ConfigureOptions<CorsOptionsSetup>();

        services.AddCatalogServices(configuration , env);

        services.AddSecurityServices(configuration, env);




    }
}