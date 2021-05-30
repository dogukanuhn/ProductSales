using MediatR;
using ProductSales.Application.Dtos;
using ProductSales.Application.Exceptions;
using ProductSales.Domain.Abstract.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Products.Commands
{
    public class UpdateProductCommand : INotification
    {
        public ProductDTO Product { get; set; }
    }
    public class UpdateProductCommandHandler : INotificationHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;


        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public async Task Handle(UpdateProductCommand notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(x => x.Sku == notification.Product.Sku);

            if (product == null)
                throw new ProductNotFoundException();

            product.ProductName = notification.Product.ProductName;
            product.Qty = notification.Product.Qty;
            product.Pricing = notification.Product.Pricing;
            product.CategoryName = notification.Product.CategoryName;

            await _productRepository.UpdateAsync(product, x => x.Sku == notification.Product.Sku);
        }
    }
}
