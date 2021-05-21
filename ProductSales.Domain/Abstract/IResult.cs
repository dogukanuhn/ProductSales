namespace ProductSales.Domain.Abstract
{
    public interface IResult
    {
        public bool Status { get; }
        public string Message { get; }

    }
}
