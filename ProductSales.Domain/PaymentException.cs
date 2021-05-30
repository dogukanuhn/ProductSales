using System;
using System.Runtime.Serialization;

namespace ProductSales.Domain
{
    [Serializable]
    public class PaymentException : Exception
    {
        public PaymentException()
        {
        }

        public PaymentException(string message) : base(message)
        {
        }

        public PaymentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaymentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
