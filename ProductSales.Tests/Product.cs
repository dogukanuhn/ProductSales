using MediatR;
using Moq;
using ProductSales.Application.Products.Commands;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductSales.Tests
{
    public class Product
    {

        Price pricing = new Price()
        {
            BasePrice = 10.0M,
            Discount = 5.0M,
            SellPrice = 5.0M
        };

        [Theory]
        [InlineData("test", 10, "Kg", "Bez")]
        public async Task AddProductValidFormat(string productName, short Qty, string Unit, string CategoryName)
        {   
                var fake = new Mock<IMediator>();
                var repo = new Mock<IProductRepository>();
                var repo2 = new Mock<ISellerRepository>();

                AddProductCommand command = new()
                {
                    ProductName = productName,
                    CategoryName = CategoryName,
                    Pricing = pricing,
                    Qty = Qty,
              
                    Unit = Unit
                };
                AddProductCommandHandler handler = new(repo.Object, repo2.Object);
                bool result = await handler.Handle(command, new CancellationToken());
                Assert.True(result);              
        }

        [Theory]
        [InlineData("", 0, "Kg", "Bez")]
        [InlineData("test", 10, "", "")]
        [InlineData("test", 10, "", "Bez")]
        public async Task AddProductInValid(string productName, short qty, string unit, string categoryName)
        {      
                var fake = new Mock<IMediator>();
                var repo = new Mock<IProductRepository>();
                var repo2 = new Mock<ISellerRepository>();
                AddProductCommand command = new()
                {
                    ProductName = productName,
                    CategoryName = categoryName,
                    Pricing = pricing,
                    Qty = qty,
                    Unit = unit
                };
                AddProductCommandHandler handler = new(repo.Object, repo2.Object);
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}
