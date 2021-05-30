using MediatR;
using ProductSales.Application.Exceptions.User;
using ProductSales.Application.Helpers;
using ProductSales.Application.Services;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Customers.Queries
{
    public class LoginCustomerQuery : IRequest<IDataResult<Dictionary<string, string>>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class LoginCustomerQueryHandler : IRequestHandler<LoginCustomerQuery, IDataResult<Dictionary<string, string>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtHandler _jwtHandler;

        public LoginCustomerQueryHandler(ICustomerRepository customerRepository, IJwtHandler jwtHandler)
        {
            _customerRepository = customerRepository;
            _jwtHandler = jwtHandler;
        }
        public async Task<IDataResult<Dictionary<string, string>>> Handle(LoginCustomerQuery request, CancellationToken cancellationToken)
        {
            var user = await _customerRepository.GetAsync(x => x.Email == request.Email);

            if (user is null)
                throw new UserNotFoundException();

            var passwordStatus = HashService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if (passwordStatus == false)
                throw new WrongPasswordException();

            var token = _jwtHandler.Authenticate(user);
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("token", token);
            return new DataResult<Dictionary<string, string>>(response, true);
        }
    }
}
