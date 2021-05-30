using FluentValidation;
using ProductSales.Application.Products.Commands;

namespace ProductSales.Application.Validations.ProductValidation
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidation()
        {
            RuleFor(x => x.Product.Sku).NotEmpty();
        }
    }
}
