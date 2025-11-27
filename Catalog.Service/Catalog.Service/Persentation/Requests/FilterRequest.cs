namespace Catalog.Service.Persentation.Requests;

public sealed record FilterRequest(string? Criteria,string? Sort , int PageSize, int PageIndex);
