using System;

namespace ProductSales.Domain.Abstract
{
    public interface IUser
    {
        public Guid Code { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
