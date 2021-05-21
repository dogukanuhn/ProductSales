using MediatR;
using ProductSales.Application.Services;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Sellers.Commands
{
    public class RegisterSellerCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string Phone { get; set; }
        public string Brand { get; set; }

        public List<Address> Address { get; set; }

    }

    public class RegisterSellerCommandHandler : IRequestHandler<RegisterSellerCommand, IResult>
    {
        private readonly ISellerRepository _sellerRepository;
        public RegisterSellerCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }
        public async Task<IResult> Handle(RegisterSellerCommand request, CancellationToken cancellationToken)
        {

            AuthenticationService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var seller = Seller.Register(request.FirstName, request.LastName, request.Phone
                , request.Email, request.Brand, passwordSalt, passwordHash);



            await _sellerRepository.AddAsync(seller, cancellationToken);

            return new Result(true);

        }
    }
}
