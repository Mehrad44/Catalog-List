using AutoMapper;
using Catalog.Service.Catalog.Application.Products.Contracts.Dtos;
using Catalog.Service.Catalog.Domain.Product;

namespace Catalog.Service.Catalog.Application.Products
{
    public class ProductMapper : Profile
    {

        public ProductMapper()
        {
            CreateMap<Product, ProductDto>(); 
        }

    }
}
