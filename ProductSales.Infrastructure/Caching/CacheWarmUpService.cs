using MediatR;
using ProductSales.Application.Products.Queries;
using ProductSales.Domain.Abstract;
using Serilog;
using System.Threading.Tasks;

namespace ProductSales.Infrastructure.Caching
{
    public class CacheWarmUpService : ICacheWarmUpService
    {
        private readonly IMediator _mediator;
        public CacheWarmUpService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task WarmUp()
        {
            await _mediator.Send(new GetProductListQuery());
            Log.Information("{Data} Warm Up On Start", "Product List");

        }
    }
}
