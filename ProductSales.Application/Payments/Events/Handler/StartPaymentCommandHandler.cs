using MediatR;
using ProductSales.Application.Constants;

using ProductSales.Application.Services;

using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Abstract.Services;
using ProductSales.Domain.Models;

using System.Text;
using System.Text.Json;

using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Payments.Events.Handler
{
    public class StartPaymentCommandHandler : INotificationHandler<CreatePaymentNotification>

    {
        private readonly IPaymentService _paymentService;
        private readonly ISellerRepository _sellerRepository;
        private readonly IBusService _busService;

        public StartPaymentCommandHandler(IPaymentService paymentService, ICustomerRepository customerRepository, ISellerRepository sellerRepository, IBusService busService)
        {
            _paymentService = paymentService;
            _sellerRepository = sellerRepository;
            _busService = busService;
        }
        public async Task Handle(CreatePaymentNotification notification, CancellationToken cancellationToken)
        {

            CustomerPayment customerPayment = new(notification.BasketCode, notification.Price, notification.PaidPrice, notification.CustomerCode,
                notification.BasketItems, notification.BillingAddress, notification.ShippingAddress, notification.PaymentCard, notification.IP, notification.SellerCode);
            await _paymentService.Create(customerPayment);


            var seller = await _sellerRepository.GetAsync(x => x.Code == notification.SellerCode);

            StringBuilder products = new StringBuilder();
           

            foreach (var item in notification.BasketItems)
            {

                products.AppendLine($"{item.Name} ürününüzden 1 adet satılmıştır.");
            }
            var message = new Message { MessageText = products.ToString() };

            if (seller.Notifications.Email)
            {
                message.To = seller.Email;
                _busService.PublishToMessageQueue("notification", $"notification.{NotificationTypes.EMAIL}", JsonSerializer.Serialize(message));

            }
            if (seller.Notifications.Sms)
            {
  
                message.To = seller.Phone;
                _busService.PublishToMessageQueue("notification", $"notification.{NotificationTypes.SMS}", JsonSerializer.Serialize(message));
            }


            

        }

        public class Message
        {
            public string MessageText { get; set; }
            public string To { get; set; }

        }
    }
}
