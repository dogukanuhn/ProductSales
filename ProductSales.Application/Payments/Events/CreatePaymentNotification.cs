using MediatR;
using ProductSales.Domain.Models;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProductSales.Application.Payments.Events
{
    public class CreatePaymentNotification : INotification
    {
        public Guid BasketCode { get; set; }
        public Guid SellerCode { get; set; }
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public PaymentCard PaymentCard { get; set; }
        public Guid CustomerCode { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public List<State> States { get; set; }

        public string IP { get; set; }
    }

}
