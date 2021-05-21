using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.BusinessRules;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProductSales.Domain.Models
{
    public class Customer : BaseModel, IUser
    {
        public Customer(string name, string surname, string email, string identityNumber, List<Address> address, ICustomerUniqunessChecker customerUniqunessChecker, byte[] passwordSalt, byte[] passwordHash)
        {
            CheckRule(new CustomerMustBeUniqueRule(customerUniqunessChecker, email));
            Name = name;
            Surname = surname;
            Email = email;
            Code = Guid.NewGuid();
            IdentityNumber = identityNumber;
            Address = address;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
        }

        [BsonRepresentation(BsonType.String)]
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<Address> Address { get; set; }
        public string IdentityNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }


    }
}
