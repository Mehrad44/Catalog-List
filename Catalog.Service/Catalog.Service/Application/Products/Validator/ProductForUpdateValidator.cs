using Catalog.Service.Application.Products.Dtos;
using FluentValidation;

namespace Catalog.Service.Application.Products.Validator
{
    public class ProductForUpdateValidator : AbstractValidator<ProductForUpdateDto>
    {
        public ProductForUpdateValidator()
        {
            RuleFor(p => p.Name)
             .NotEmpty()
             .NotNull()
             .MinimumLength(3)
             .MaximumLength(10);
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);

            RuleFor(p => p.Description)
                .MaximumLength(100);
        }
    }
}
