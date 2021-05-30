using System;
using System.Runtime.Serialization;

namespace ProductSales.Domain.Exceptions
{
    public class PaymentFailureException : PaymentException
    {
        public PaymentFailureException()
        {
        }

        public PaymentFailureException(string message) : base(message)
        {
        }

        public PaymentFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaymentFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
