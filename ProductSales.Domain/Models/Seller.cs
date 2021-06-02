using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductSales.Application.Exceptions;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductSales.Domain.Models
{
    public class Seller : BaseModel, IUser
    {

        [BsonRepresentation(BsonType.String)]
        public Guid Code { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Brand { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Notification Notifications { get; set; }
        public List<Address> ActiveAddress { get; set; }
        public Seller(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

        }


        public static Seller Register(string firstName, string lastName, string phone, string email, string brand, byte[] salt, byte[] hash)
        {
            var merchant = new Seller(firstName, lastName)
            {
                Code = Guid.NewGuid(),
                ActiveAddress = new List<Address>(),
                Brand = brand,
                Phone = phone,
                Email = email,
                PasswordSalt = salt,
                PasswordHash = hash,
                IsActive = true,
                IsDeleted = false,
                Notifications = new Notification { Email=true,Sms=false}
            };
            return merchant;
        }

        public void DeleteSeller()
        {
            IsActive = false;
            IsDeleted = true;

        }

        public void DeactivateSeller()
        {
            IsActive = false;
        }
        public void AddActiveAddress(Address adress)
        {
            ActiveAddress.Add(adress);

        }
        public void RemoveActiveAddress(Address adress)
        {
            if (!ActiveAddress.Any(x => x.ContactName == adress.ContactName))
                throw new ActiveAddressNameNotfoundException();
            ActiveAddress.Remove(adress);

        }
    }
}