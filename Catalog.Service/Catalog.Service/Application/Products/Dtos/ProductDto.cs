namespace Catalog.Service.Application.Products.Dtos;

public sealed record ProductDto(Guid Id,string Name , decimal Price , string Description);