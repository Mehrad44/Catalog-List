using Carter;
using Catalog.Service;
using Catalog.Service.Application.Contracts;
using Catalog.Service.Application.Products;
using Catalog.Service.Domain.Product;
using Catalog.Service.Infrustructure.Persistence;
using Catalog.Service.Infrustructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddServices(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
