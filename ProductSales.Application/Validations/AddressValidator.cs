using FluentValidation;
using ProductSales.Domain.ValueObjects;

namespace ProductSales.Application.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ContactName).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();





        }
    }
}
