using AutoMapper;
using Catalog.Service.Application.Products.Dtos;
using Catalog.Service.Domain.Product;

namespace Catalog.Service.Application.Products
{
    public class ProductMapper : Profile
    {

        public ProductMapper()
        {
            CreateMap<Product, ProductDto>(); 
        }

    }
}
