using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
