using System.Threading.Tasks;

namespace ProductSales.Domain.Abstract
{
    public interface ICacheWarmUpService
    {
        Task WarmUp();
    }
}
