using MediatR;
using ProductSales.Application.Exceptions;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using ProductSales.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Products.Queries
{
    public class GetProductByNameQuery : IRequest<IDataResult<Product>>
    {
        public string ProductName { get; set; }
    }
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IDataResult<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IDataResult<Product>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(x => x.ProductName == request.ProductName);

            if (product is null)
                throw new ProductNotFoundException("test");


            return new DataResult<Product>(product, true);
        }
    }
}
