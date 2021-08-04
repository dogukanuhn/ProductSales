using MediatR;
using ProductSales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Domain.Events
{
    public class PaymentReceivedEvent : INotification
    {
        public string Message { get; set; }
        public Notification Notification { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }


        public PaymentReceivedEvent(string message, Notification notification, string email, string phone)
        {
            Message = message;
            Notification = notification;
            Email = email;
            Phone = phone;
        }






    }

}
