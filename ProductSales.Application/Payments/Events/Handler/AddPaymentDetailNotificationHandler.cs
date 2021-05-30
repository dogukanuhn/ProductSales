using MediatR;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Payments.Events.Handler
{
    class AddPaymentDetailNotificationHandler : INotificationHandler<CreatePaymentNotification>
    {
        private readonly IPaymentRepository _paymentRepository;


        public AddPaymentDetailNotificationHandler(IPaymentRepository paymentRepository, ICustomerRepository customerRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(CreatePaymentNotification notification, CancellationToken cancellationToken)
        {
            CustomerPayment customerPayment = new(notification.BasketCode, notification.Price, notification.PaidPrice, notification.CustomerCode,
                notification.BasketItems, notification.ShippingAddress, notification.ShippingAddress);

            await _paymentRepository.AddAsync(customerPayment, cancellationToken);


        }
    }
}
