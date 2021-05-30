using FluentValidation;
using ProductSales.Application.Products.Commands;

namespace ProductSales.Application.Validations.ProductValidation
{
    public class DeleteProductValidation : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidation()
        {
            RuleFor(x => x.Sku).NotEmpty();
        }
    }
}
