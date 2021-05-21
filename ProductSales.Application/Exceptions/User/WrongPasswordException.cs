using ProductSales.Domain;
using System;
using System.Runtime.Serialization;

namespace ProductSales.Application.Exceptions.User
{
    public class WrongPasswordException : AuthenticationException
    {
        public WrongPasswordException()
        {
        }

        public WrongPasswordException(string message) : base(message)
        {
        }

        public WrongPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
