using ProductSales.Domain.Abstract;

namespace ProductSales.Domain.Concrete
{
    public class Result : IResult
    {
        public bool Status { get; }
        public string Message { get; }

        public Result(bool status)
        {
            Status = status;
        }
        public Result(bool status, string message) : this(status)
        {
            Message = message;
        }

    }
}
