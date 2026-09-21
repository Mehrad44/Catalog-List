namespace Catalog.Service.Catalog.Application.Contracts.Data;

public record QueryData(string? Sort, int PageSize , int PageIndex);

