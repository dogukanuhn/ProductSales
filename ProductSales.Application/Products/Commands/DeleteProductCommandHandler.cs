using MediatR;
using ProductSales.Application.Exceptions;
using ProductSales.Domain.Abstract.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Products.Commands
{
    public class DeleteProductCommand : INotification
    {
        public string Sku { get; set; }

    }
    public class DeleteProductCommandHandler : INotificationHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISellerProductRepository _sellerProductRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository, ISellerProductRepository sellerProductRepository)
        {
            _productRepository = productRepository;
            _sellerProductRepository = sellerProductRepository;
        }
        public async Task Handle(DeleteProductCommand notification, CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetAsync(x => x.Sku == notification.Sku);

            if (product == null)
                throw new ProductNotFoundException();

            product.DeleteProduct();
            var sProduct = await _sellerProductRepository.GetAsync(x => x.Sku == notification.Sku);
            sProduct.DeactivateProduct();
            await _sellerProductRepository.UpdateAsync(sProduct, x => x.Sku == notification.Sku);

            await _productRepository.UpdateAsync(product, x => x.Sku == notification.Sku);


        }
    }
}
