using MediatR;
using Microsoft.AspNetCore.Authentication;
using Moq;
using ProductSales.Application.Customers.Commands;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.BusinessRules;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductSales.Tests
{
    public class Customer
    {
        List<Address> address = new()
        {
            new Address
            {
                City = "Ýstanbul",
                ContactName = "Test test123",
                Country = "Turkey",
                Description = "ev adresi",
                ZipCode = "34444"
            }
        };

        [Theory]
        [InlineData("test", "test123", "a@asdf.com", "32534785658", "test123.", "test123.")]
        public async Task RegisterCustomer_ReturnUnit(string firstName, string lastName, string email, string identity, string password, string passwordAgain)
        {
  
                var fake = new Mock<IMediator>();
                var repo = new Mock<ICustomerRepository>();
                var service = new Mock<ICipherService>();
                var rule = new Mock<ICustomerUniqunessChecker>();
                RegisterCustomerCommand command = new()
                {
                    FirstName = firstName,
                    Address = address,
                    Email = email,
                    IdentityNumber = identity,
                    LastName = lastName,
                    Password = password,
                    PasswordAgain = passwordAgain
                };
                RegisterCustomerCommandHandler handler = new(repo.Object,service.Object, rule.Object);      
                Unit x = await handler.Handle(command, new CancellationToken());

                Assert.Equal(Unit.Task.Result, x);

           
        }


    
    }
}
