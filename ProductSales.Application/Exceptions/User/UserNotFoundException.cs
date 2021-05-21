using ProductSales.Domain;
using System;
using System.Runtime.Serialization;

namespace ProductSales.Application.Exceptions.User
{
    [Serializable]
    public class UserNotFoundException : AuthenticationException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
