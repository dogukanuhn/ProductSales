﻿using MongoDB.Bson.Serialization.Attributes;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProductSales.Domain.Models
{
    public class CustomerPayment : BaseModel
    {
        public CustomerPayment(Guid basketCode, decimal price, decimal paidPrice, Guid customerCode, List<BasketItem> basketItems, Address billingAddress, Address shippingAddress)
        {
            BasketCode = basketCode;
            Price = price;
            PaidPrice = paidPrice;
            Curreny = "TRY";
            Installment = 1;
            CustomerCode = customerCode;
            BasketItems = basketItems;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;

        }
        public CustomerPayment(Guid basketCode, decimal price, decimal paidPrice, Guid customerCode, List<BasketItem> basketItems, Address billingAddress, Address shippingAddress, PaymentCard paymentCard, string ıP)
        {
            BasketCode = basketCode;
            Price = price;
            PaidPrice = paidPrice;
            Curreny = "TRY";
            Installment = 1;
            CustomerCode = customerCode;
            BasketItems = basketItems;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            PaymentCard = paymentCard;
            IP = ıP;
        }



        public Guid BasketCode { get; set; }
        public Guid CustomerCode { get; set; }
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public string Curreny { get; set; }
        public string IP { get; set; }

        public int Installment { get; set; }
        [BsonIgnore]
        public PaymentCard PaymentCard { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }

        public List<State> States { get; set; }
    }

    public class BasketItem
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }

        public string ItemType { get; set; }
        public decimal Price { get; set; }


    }


    public class PaymentCard
    {
        public string CardHoldername { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }
        public int RegisterdCard { get; set; }

    }
}
