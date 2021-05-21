using ProductSales.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace ProductSales.Application.Exceptions
{
    public class ActiveAddressNameNotfoundException : NotFoundException
    {
        public ActiveAddressNameNotfoundException()
        {
        }

        public ActiveAddressNameNotfoundException(string message) : base(message)
        {
        }

        public ActiveAddressNameNotfoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActiveAddressNameNotfoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
