using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using ProductSales.Application.Products.Commands;
using ProductSales.Application.Products.Queries;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using ProductSales.Infrastructure;
using ProductSales.Infrastructure.Config;
using ProductSales.Infrastructure.Interfaces;
using ProductSales.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductSales.Tests
{
    public class ProductTest
    {
        private Mock<IOptions<MongoDbSettings>> _mockOptions;
        private Mock<IMongoDatabase> _mockDB;
        private Mock<IMongoClient> _mockClient;

        public ProductTest()
        {
            _mockOptions = new Mock<IOptions<MongoDbSettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }


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
        [InlineData("test", 10, "asdf", "")]
        public async Task AddProduct_InvalidValues_ThrowException(string productName, short qty, string unit, string categoryName)
        {      
        
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

        [Theory]
        [InlineData("Kalem")]
        public async Task GetProduct_ByName_ReturnTrue(string productName)
        {

            var settings = new MongoDbSettings()
            {
                ConnectionString= "mongodb://localhost:27017",
                Database="ProductSales"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.Database, null))
                .Returns(_mockDB.Object);

            var repo = new Mock<ProductRepository>(new DbContext<Product>(_mockOptions.Object));

            GetProductByNameQuery command = new()
            {
                ProductName = productName,
               
            };
            GetProductByNameQueryHandler handler = new(repo.Object);
            var result = await handler.Handle(command, new CancellationToken());
            Assert.True(result.Status);
        }


    }
}
