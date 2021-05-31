
using ProductSales.Application.Exceptions.User;
using ProductSales.Application.Helpers;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Abstract.Services;
using ProductSales.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Application.Services
{
    public  class AuthenticationService  : IAuthenticationService
    {
        private  ICustomerRepository _customerRepository;
        private  ISellerRepository _sellerRepository;
        private  IJwtHandler _jwtHandler;

        public  AuthenticationService(ICustomerRepository customerRepository, ISellerRepository sellerRepository, IJwtHandler jwtHandler)
        {
            _customerRepository = customerRepository;
            _sellerRepository = sellerRepository;
            _jwtHandler = jwtHandler;
        }

        public  async Task<IDataResult<Dictionary<string, string>>> LoginSeller(string email,string password)
        {
            var user = await _sellerRepository.GetAsync(x => x.Email == email);

            if (user is null)
                throw new UserNotFoundException();


            return ValidatePasswordAndToken(user, password);
        }


        public  async Task<IDataResult<Dictionary<string, string>>> LoginCustomer(string email, string password)
        {
            var user = await _customerRepository.GetAsync(x => x.Email == email);

            if (user is null)
                throw new UserNotFoundException();

            return ValidatePasswordAndToken(user, password);


        }

        private  IDataResult<Dictionary<string, string>> ValidatePasswordAndToken(IUser user, string password)
        {
            var passwordStatus = HashService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

            if (passwordStatus == false)
                throw new WrongPasswordException();

            var token = _jwtHandler.Authenticate(user);
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("token", token);
            return new DataResult<Dictionary<string, string>>(response, true);
        }


    }
}
