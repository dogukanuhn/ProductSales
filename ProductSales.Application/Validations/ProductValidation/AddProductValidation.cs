using FluentValidation;
using ProductSales.Application.Products.Commands;

namespace ProductSales.Application.Validations.ProductValidation
{
    public class AddProductValidation : AbstractValidator<AddProductCommand>
    {
        public AddProductValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Qty).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();









        }
    }
}
