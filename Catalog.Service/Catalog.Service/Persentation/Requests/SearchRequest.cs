namespace Catalog.Service.Persentation.Requests
{
    public sealed record SearchRequest(string? text, string? Sort, int PageSize, int PageIndex);
}
