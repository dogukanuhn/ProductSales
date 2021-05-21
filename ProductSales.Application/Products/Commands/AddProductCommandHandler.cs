using MediatR;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Products.Commands
{
    public class AddProductCommand : IRequest<IResult>
    {
        public string ProductName { get; set; }
        public short Qty { get; set; }
        public Price Pricing { get; set; }

        public string Unit { get; set; }
        public string CategoryName { get; set; }
        public string SellerCode { get; set; }



    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, IResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;



        public AddProductCommandHandler(IProductRepository productRepository, ISellerRepository sellerRepository)
        {
            _productRepository = productRepository;
            _sellerRepository = sellerRepository;
        }
        public async Task<IResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.ProductName, request.Qty, request.CategoryName, request.Pricing, request.Unit);


            var result = await _productRepository.AddAsync(product, cancellationToken);

            if (result != null)
                return new Result(true, "Added Successfully");

            return new Result(false, "Failure");

        }
    }
}
