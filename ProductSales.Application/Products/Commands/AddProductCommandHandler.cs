using MediatR;
using ProductSales.Application.Exceptions.Product;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Products.Commands
{
    public class AddProductCommand : IRequest<bool>
    {
        public string ProductName { get; set; }
        public short Qty { get; set; }
        public Price Pricing { get; set; }

        public string Unit { get; set; }
        public string CategoryName { get; set; }

    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand,bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;



        public AddProductCommandHandler(IProductRepository productRepository, ISellerRepository sellerRepository)
        {
            _productRepository = productRepository;
            _sellerRepository = sellerRepository;
        }
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductName is null || request.CategoryName is null || request.Unit is null)
                throw new ArgumentOutOfRangeException();


            var product = new Product(request.ProductName, request.Qty, request.CategoryName, request.Pricing, request.Unit);


            var result = await _productRepository.AddAsync(product, cancellationToken);

            if (result != null)
                throw new Exception();

            return true;
        }
    }
}
