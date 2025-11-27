using Carter;
using Catalog.Service.Application.Contracts;
using Catalog.Service.Application.Contracts.Data;
using Catalog.Service.Application.Products.Dtos;
using Catalog.Service.Persentation.Requests;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Catalog.Service.Persentation
{
    public class ProductEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var products = app.MapGroup("products").WithTags("Products");

            products.MapPost("/", CreateProductAsync);
            products.MapGet("/", GetProductsAsync);
            products.MapGet("/{id}", GetProductByIdAsync);
            products.MapPut("/{id}", UpdateProductAsync);
            products.MapDelete("/{id}", DeleteProductdAsync);
            products.MapGet("/filter", FilterProductAsync);
            products.MapGet("/Search", SearchProductAsync);
        }


        private async Task<IResult> CreateProductAsync(
            [FromBody] ProductForCreateDto productDto,
            [FromServices] IProductManager productManager)
        {

            var result = await productManager.CreateProductAsync(productDto);
            if (result.IsSuccess)
            {
                return Results.Ok();

            }
            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);
            if (validationResult is not null)
            {
                return Results.ValidationProblem(validationResult!.ToDictionary());

            }

            return Results.BadRequest(result.Error);




        }

        private async Task<IResult> GetProductsAsync([FromServices] IProductManager productManager)
        {


            var productsResult = await productManager.GetProductsAsync();

            if (productsResult.IsSuccess)
            {
                return Results.Ok(productsResult);

            }


            return Results.Ok(productsResult.Value);

        }

        private async Task<IResult> GetProductByIdAsync(Guid id, [FromServices] IProductManager productManager)
        {

            var productsResult = await productManager.GetProductByIdAsync(id);

            if (productsResult.IsSuccess)
            {
                return Results.Ok(productsResult.Value);

            }


            return Results.Ok(productsResult.Value);
        }


        private async Task<IResult> DeleteProductdAsync(Guid id, [FromServices] IProductManager productManager)
        {

            var productsResult = await productManager.GetProductByIdAsync(id);

            if (productsResult.IsSuccess)
            {
                return Results.Ok(productsResult.Value);

            }


            return Results.Ok(productsResult.Value);
        }


        private async Task<IResult> UpdateProductAsync(
         [FromRoute] Guid productId,
        [FromBody] ProductForUpdateDto productDto,
        [FromServices] IProductManager productManager)
        {

            var result = await productManager.UpdateProductAsync(productId , productDto);
            if (result.IsSuccess)
            {
                return Results.Ok();

            }
            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);
            if (validationResult is not null)
            {
                return Results.ValidationProblem(validationResult!.ToDictionary());

            }

            return Results.BadRequest(result.Error);




        }


        private async Task<IResult> FilterProductAsync(
            [AsParameters] FilterRequest filter,
            [FromServices] IProductManager productManager,
            HttpResponse response)
        {
           await Task.Delay(1500);

            var productsPagedListResult = await productManager.FilterProductAsync(filter.Criteria, new QueryData(filter.Sort , filter.PageSize , filter.PageIndex));

            if (productsPagedListResult.IsSuccess)
            {
                response.Headers.Append("x-TotalRecordCount",
                    productsPagedListResult.Value.TotalRecordCount.ToString());
                return TypedResults.Ok(productsPagedListResult.Value.Items);



            }


            return Results.Ok(productsPagedListResult.Error);

        }

        private async Task<IResult> SearchProductAsync(
            [AsParameters] SearchRequest search,
            [FromServices] IProductManager productManager,
            HttpResponse response)
        {


            var productsPagedListResult = await productManager.SearchProductAsync(search.text, new QueryData(search.Sort, search.PageSize, search.PageIndex));

            if (productsPagedListResult.IsSuccess)
            {
                response.Headers.Append("x-TotalRecordCount",
                    productsPagedListResult.Value.TotalRecordCount.ToString());
                return TypedResults.Ok(productsPagedListResult.Value.Items);



            }


            return Results.Ok(productsPagedListResult.Error);

        }



    }
}
