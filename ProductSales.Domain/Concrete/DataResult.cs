using ProductSales.Domain.Abstract;

namespace ProductSales.Domain.Concrete
{
    public class DataResult<T> : Result, IDataResult<T> where T : class, new()
    {
        public T Data { get; }

        public DataResult(T data, bool succes, string message) : base(succes, message)
        {
            Data = data;
        }
        public DataResult(T data, bool succes) : base(succes)
        {
            Data = data;
        }

    }
}
