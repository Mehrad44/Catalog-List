using AutoMapper;
using Catalog.Service.Application.Contracts;
using Catalog.Service.Application.Contracts.Data;
using Catalog.Service.Application.Products.Dtos;
using Catalog.Service.Domain.Product;
using CSharpFunctionalExtensions;
using FluentValidation;
using System.Text.Json;

namespace Catalog.Service.Application.Products
{
    public class ProductManager : IProductManager
    {
        private IValidator<ProductForCreateDto> _productForCreateValidator;
        private readonly IProductReposioty _productReposioty;
        private readonly IMapper _mapper;
        private IValidator<ProductForUpdateDto> _productForUpdateValidator;


        public ProductManager(IProductReposioty productReposioty , IMapper mapper , IValidator<ProductForCreateDto> productForCreateValidator , IValidator<ProductForUpdateDto> productForUpdateValidator)
        {
            _productReposioty = productReposioty;
            _mapper = mapper;
            _productForCreateValidator = productForCreateValidator;
            _productForUpdateValidator = productForUpdateValidator; 
        }
        public async Task<Result> CreateProductAsync(ProductForCreateDto productDto)
        {
            try
            {
                var validationResult = _productForCreateValidator.Validate(productDto);
                if(!validationResult.IsValid)
                {
                    //validationResult.Errors 

                    return Result.Failure(JsonSerializer.Serialize(validationResult));
                }
                Product product = new(productDto.Name, productDto.Price, productDto.Description);


                await _productReposioty.AddAsync(product);

                return Result.Success();
            } catch (System.Exception exp)
            {
                return Result.Failure(exp.Message);
            }

          
          
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(Guid productId)
        {
            try
            {
                var product = await _productReposioty.GetByIdAsync(productId);

                if (product is null)
                {
                    return Result.Failure<ProductDto>($"Produict Id ({productId}) not found");

                }

                //ProductDto productDto = new(product.Id, product.Name, product.Price, product.Description);

                var productDto = _mapper.Map<ProductDto>(product);

                return Result.Success(productDto);
            }
            catch(System.Exception ex)
            {
                return Result.Failure<ProductDto>(ex.Message);
            }
           
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetProductsAsync()
        {
            try
            {
                IEnumerable<Product> products = await _productReposioty.FindEntitiesAsync(p => p.Price > 50);


                //var productResult = products.Select(product => new ProductDto(product.Id, product.Name, product.Price, product.Description)
                //    );

                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

                //return Result.Success(productResult);

                return Result.Success(productsDto );


            }
            catch (System.Exception exp)
            {
                return Result.Failure<IEnumerable<ProductDto>>(exp.Message);
            }
     
        }

        public async Task<Result> UpdateProductAsync(Guid productId , ProductForUpdateDto productDto)
        {
            try
            {
                var validationResult = _productForUpdateValidator.Validate(productDto);
                if (!validationResult.IsValid)
                {
                    //validationResult.Errors 

                    return Result.Failure(JsonSerializer.Serialize(validationResult));
                }


                var product = await _productReposioty.GetByIdAsync(productId);
                
                if(product is null)
                {
                    return Result.Failure($"Product ID {productId} is not found");

                }
                product.ChangePrice(productDto.Price);
                product.ChangeName(productDto.Name);
                product.Description = productDto.Description;

                await _productReposioty.AddAsync(product);

                return Result.Success();
            }
            catch (System.Exception exp)
            {
                return Result.Failure(exp.Message);
            }



        }

        public async Task<Result> DeleteProductAsync(Guid productId)
        {
            try
            {
                var product = await _productReposioty.GetByIdAsync(productId);

                if (product is null)
                {
                    return Result.Failure<ProductDto>($"Produict Id ({productId}) not found");

                }



              await  _productReposioty.DeleteAsync(product);
                return Result.Success();
            }
            catch (System.Exception ex)
            {
                return Result.Failure<ProductDto>(ex.Message);
            }
        }

        public async Task<Result<PagedList<ProductDto>>> FilterProductAsync(string? criteria,  QueryData data)
        {
            try
            {
                var (products, totalRecordCount)  = await _productReposioty.FilterAsunc(criteria ,data.Sort , data.PageSize , data.PageIndex);


                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                
                var pagedList = new PagedList<ProductDto>(productDtos, totalRecordCount);

                return Result.Success(pagedList);
               
            }
            catch (System.Exception ex)
            {
                return Result.Failure<PagedList<ProductDto>>(ex.Message);
            }
        }


        public async Task<Result<PagedList<ProductDto>>> SearchProductAsync(string? text, QueryData data)
        {
            try
            {
                var (products, totalRecordCount) = await _productReposioty.SearchAsunc(text, data.Sort, data.PageSize, data.PageIndex);


                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                var pagedList = new PagedList<ProductDto>(productDtos, totalRecordCount);

                return Result.Success(pagedList);

            }
            catch (System.Exception ex)
            {
                return Result.Failure<PagedList<ProductDto>>(ex.Message);
            }
        }
    }
}
