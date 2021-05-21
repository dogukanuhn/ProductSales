using MediatR;
using ProductSales.Application.Constants;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Concrete;
using ProductSales.Domain.Models;
using Serilog;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Products.Queries
{
    public class GetProductListQuery : IRequest<IDataResult<List<Product>>>
    {

    }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IDataResult<List<Product>>>
    {


        private readonly IProductRepository _productRepository;
        private readonly ICacheManager _cacheManager;
        public GetProductListQueryHandler(IProductRepository productRepository, ICacheManager cacheManager)
        {
            _productRepository = productRepository;
            _cacheManager = cacheManager;
        }


        public async Task<IDataResult<List<Product>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {

            var cachedData = _cacheManager.TryGet(CacheKeys.PRODUCT_ALL);
            List<Product> cachedList;

            if (cachedData != null)
            {
                cachedList = JsonSerializer.Deserialize<List<Product>>(cachedData);
            }
            else
            {
                cachedList = await _productRepository.GetAllAsync();
                Log.Information("{Data} Form DB", "Product List");
                _cacheManager.Set(CacheKeys.PRODUCT_ALL, JsonSerializer.Serialize(cachedList));

            }


            return new DataResult<List<Product>>(cachedList, true);

        }
    }
}
