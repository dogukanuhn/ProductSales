namespace ProductSales.Domain.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
