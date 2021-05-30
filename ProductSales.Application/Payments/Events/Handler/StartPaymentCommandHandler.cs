using MediatR;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Abstract.Services;
using ProductSales.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Payments.Events.Handler
{
    public class StartPaymentCommandHandler : INotificationHandler<CreatePaymentNotification>

    {
        private readonly IPaymentService _paymentService;

        public StartPaymentCommandHandler(IPaymentService paymentService, ICustomerRepository customerRepository, ICipherService cipherService)
        {
            _paymentService = paymentService;

        }
        public async Task Handle(CreatePaymentNotification notification, CancellationToken cancellationToken)
        {



            CustomerPayment customerPayment = new(notification.BasketCode, notification.Price, notification.PaidPrice, notification.CustomerCode,
                notification.BasketItems, notification.BillingAddress, notification.ShippingAddress, notification.PaymentCard, notification.IP);
            _paymentService.Create(customerPayment);



        }
    }
}
