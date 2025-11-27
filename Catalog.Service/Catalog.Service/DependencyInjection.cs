using Carter;
using Catalog.Service.Application.Contracts;
using Catalog.Service.Application.Products;
using Catalog.Service.Domain.Product;
using Catalog.Service.Infrustructure.Persistence;
using Catalog.Service.Infrustructure.Persistence.Repositories;
using Catalog.Service.Infrustructure.Security.Options;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Service;

public static class DependencyInjection
{
     public static void AddServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCarter();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped <IProductReposioty, ProductSqlRepository>();
            services.AddDbContext<CatalogContext>(setup =>
            {
                var cnnstr = configuration.GetConnectionString("Catalog");
                setup.UseSqlServer(cnnstr);
            });
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddCors();
        services.ConfigureOptions<CorsOptionsSetup>();
        


    }
}