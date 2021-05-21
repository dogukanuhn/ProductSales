using MediatR;
using ProductSales.Application.Exceptions.User;
using ProductSales.Application.Helpers;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Sellers.Queries
{
    public class LoginSellerQuery : IRequest<IDataResult<Dictionary<string, string>>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class LoginSellerQueryHandler : IRequestHandler<LoginSellerQuery, IDataResult<Dictionary<string, string>>>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IJwtHandler _jwtHandler;

        public LoginSellerQueryHandler(ISellerRepository sellerRepository, IJwtHandler jwtHandler)
        {
            _sellerRepository = sellerRepository;
            _jwtHandler = jwtHandler;
        }
        public async Task<IDataResult<Dictionary<string, string>>> Handle(LoginSellerQuery request, CancellationToken cancellationToken)
        {
            var user = await _sellerRepository.GetAsync(x => x.Email == request.Email);

            if (user is null)
                throw new UserNotFoundException();


            var token = _jwtHandler.Authenticate(user);
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("token", token);
            return new DataResult<Dictionary<string, string>>(response, true);
        }
    }
}
