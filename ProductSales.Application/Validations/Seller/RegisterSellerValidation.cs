using FluentValidation;
using ProductSales.Application.Sellers.Commands;

namespace ProductSales.Application.Validations.Seller
{
    public class RegisterSellerValidation : AbstractValidator<RegisterSellerCommand>
    {
        public RegisterSellerValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().Must(EmailValidator.EmailIsValid).WithMessage("Email error");
            RuleFor(x => x.Brand).NotEmpty();

            RuleForEach(x => x.Address).SetValidator(new AddressValidator());


            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Password).Equal(x => x.PasswordAgain).WithMessage("Parolalar Uyuşmuyor");
        }


    }
}

