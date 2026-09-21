using Carter;
using Catalog.Service.Catalog.Application.Products;
using Catalog.Service.Catalog.Application.Products.Contracts;
using Catalog.Service.Catalog.Domain.Product;
using Catalog.Service.Catalog.Infrustructure.Persistence;
using Catalog.Service.Catalog.Infrustructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;


public static class DependencyInjection
{
     public static void AddCatalogServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
           
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped <IProductReposioty, ProductSqlRepository>();
            services.AddDbContext<CatalogContext>(setup =>
            {
                var cnnstr = configuration.GetConnectionString("Catalog");
                setup.UseSqlServer(cnnstr);
            });



    }
}