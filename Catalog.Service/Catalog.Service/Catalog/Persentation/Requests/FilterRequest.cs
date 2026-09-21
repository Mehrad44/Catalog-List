namespace Catalog.Service.Catalog.Persentation.Requests;

public sealed record FilterRequest(string? Criteria,string? Sort , int PageSize, int PageIndex);
