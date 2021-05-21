using ProductSales.Domain.Abstract;

namespace ProductSales.Application.Helpers
{
    public interface IJwtHandler
    {
        string Authenticate(IUser user);
    }
}
