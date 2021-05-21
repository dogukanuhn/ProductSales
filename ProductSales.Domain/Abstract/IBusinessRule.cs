namespace ProductSales.Domain.Abstract
{
    public interface IBusinessRule
    {
        string Message { get; }
        bool IsBroken();
    }
}
