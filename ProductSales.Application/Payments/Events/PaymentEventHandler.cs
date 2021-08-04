using MediatR;
using ProductSales.Application.Constants;
using ProductSales.Application.Services;
using ProductSales.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Payments.Events
{
    public class PaymentEventHandler : INotificationHandler<PaymentReceivedEvent>
    {
        private readonly IBusService _busService;

        public PaymentEventHandler(IBusService busService)
        {
            _busService = busService;
        }

        public  Task Handle(PaymentReceivedEvent notification, CancellationToken cancellationToken)
        {
          
            var message = new Message { MessageText = notification.Message };

            if (notification.Notification.Email)
            {
                message.To = notification.Email;
                _busService.PublishToMessageQueue("notification", $"notification.{NotificationTypes.EMAIL}", JsonSerializer.Serialize(message));

            }
            if (notification.Notification.Sms)
            {

                message.To = notification.Phone;
                _busService.PublishToMessageQueue("notification", $"notification.{NotificationTypes.SMS}", JsonSerializer.Serialize(message));
            }

            return Task.CompletedTask;
        }
    }



    public class Message
    {
        public string MessageText { get; set; }
        public string To { get; set; }

    }
}
