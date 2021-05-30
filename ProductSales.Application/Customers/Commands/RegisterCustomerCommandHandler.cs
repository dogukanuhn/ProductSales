using MediatR;
using ProductSales.Application.Services;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.BusinessRules;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Customers.Commands
{
    public class RegisterCustomerCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }

        public string Password { get; set; }
        public string PasswordAgain { get; set; }

        public List<Address> Address { get; set; }

    }
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICipherService _cipherService;
        private readonly ICustomerUniqunessChecker _customerUniqunessChecker;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, ICipherService cipherService, ICustomerUniqunessChecker customerUniqunessChecker)
        {
            _customerRepository = customerRepository;
            _cipherService = cipherService;
            _customerUniqunessChecker = customerUniqunessChecker;
        }
        public async Task<Unit> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var hashedIdentity = _cipherService.Encrypt(request.IdentityNumber);
            HashService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var customter = new Customer(request.FirstName, request.LastName, request.Email, hashedIdentity,
                request.Address, _customerUniqunessChecker, passwordSalt, passwordHash);

            await _customerRepository.AddAsync(customter, cancellationToken);

            return Unit.Task.Result;

        }
    }
}
