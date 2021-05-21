using FluentValidation;
using ProductSales.Application.Customers.Commands;

namespace ProductSales.Application.Validations.Customer
{
    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().Must(EmailValidator.EmailIsValid).WithMessage("Email error");
            RuleFor(x => x.IdentityNumber).Must(IdentityValidator.VerifyIdentity).WithMessage("Identity Number is Wrong!").MaximumLength(11).NotEmpty();
            RuleForEach(x => x.Address).SetValidator(new AddressValidator()).NotEmpty();

            RuleFor(x => x.Password).Equal(x => x.PasswordAgain).WithMessage("Parolalar Uyuşmuyor");
        }
    }
}
