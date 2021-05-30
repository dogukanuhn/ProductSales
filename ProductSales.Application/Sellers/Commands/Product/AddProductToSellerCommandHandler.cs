using MediatR;
using ProductSales.Application.Exceptions;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Sellers.Commands.Product
{
    public class AddProductToSellerCommand : IRequest<Unit>
    {
        public string SellerCode { get; set; }
        public string Sku { get; set; }
        public int Qty { get; set; }
        public Price Pricing { get; set; }
        public string Unit { get; set; }


    }
    public class AddProductToSellerCommandHandler : IRequestHandler<AddProductToSellerCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISellerProductRepository _sellerProductRepository;



        public AddProductToSellerCommandHandler(IProductRepository productRepository, ISellerProductRepository sellerProductRepository)
        {
            _productRepository = productRepository;
            _sellerProductRepository = sellerProductRepository;
        }
        public async Task<Unit> Handle(AddProductToSellerCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(x => x.Sku == request.Sku);

            if (product is null)
                throw new ProductNotFoundException();

            var sellerProduct = new SellerProduct(request.SellerCode, product.ProductName, request.Qty, product.CategoryName, request.Pricing, product.Unit, product.Sku);

            await _sellerProductRepository.AddAsync(sellerProduct, cancellationToken);

            return Unit.Task.Result;
        }
    }
}
