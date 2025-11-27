using Catalog.Service.Application.Contracts.Data;
using Catalog.Service.Application.Products.Dtos;
using Catalog.Service.Domain.Product;
using CSharpFunctionalExtensions;

namespace Catalog.Service.Application.Contracts
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
