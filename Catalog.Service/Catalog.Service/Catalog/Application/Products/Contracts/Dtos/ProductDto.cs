namespace Catalog.Service.Catalog.Application.Products.Contracts.Dtos;

public sealed record ProductDto(Guid Id,string Name , decimal Price , string Description);