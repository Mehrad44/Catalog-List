using Catalog.Service.Catalog.Application.Contracts.Data;
using Catalog.Service.Catalog.Application.Products.Contracts.Dtos;
using CSharpFunctionalExtensions;

namespace Catalog.Service.Catalog.Application.Products.Contracts
{
    public interface IProductManager
    {

       Task<Result> CreateProductAsync(ProductForCreateDto productDto);

        Task<Result> UpdateProductAsync(Guid productId , ProductForUpdateDto productDto); 

        Task<Result<IEnumerable<ProductDto>>> GetProductsAsync ();

        Task<Result<ProductDto>> GetProductByIdAsync (Guid productId);

        Task<Result> DeleteProductAsync(Guid productId);

        Task<Result<PagedList<ProductDto>>> FilterProductAsync(string? criteria, QueryData data);

        Task<Result<PagedList<ProductDto>>> SearchProductAsync(string? text, QueryData data);

    }
}
