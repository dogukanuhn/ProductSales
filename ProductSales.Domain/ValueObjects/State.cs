namespace ProductSales.Domain.ValueObjects
{
    public class State : ValueObject
    {

        public PaymentStatus Status { get; set; }



    }

    public enum PaymentStatus
    {
        Completed,
        Pending,

        Canceled
    }
}
