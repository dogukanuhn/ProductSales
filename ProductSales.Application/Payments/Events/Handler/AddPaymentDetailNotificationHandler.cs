using MediatR;
using ProductSales.Application.Payments.Commands;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Application.Payments.Events.Handler
{
    class AddPaymentDetailNotificationHandler : IRequestHandler<StartPaymentCommand,Unit>
    {
        private readonly IPaymentRepository _paymentRepository;


        public AddPaymentDetailNotificationHandler(IPaymentRepository paymentRepository, ICustomerRepository customerRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(StartPaymentCommand request, CancellationToken cancellationToken)
        {
            CustomerPayment customerPayment = new(request.BasketCode, request.Price, request.PaidPrice, request.CustomerCode,
                 request.BasketItems, request.ShippingAddress, request.ShippingAddress, request.SellerCode);

            await _paymentRepository.AddAsync(customerPayment, cancellationToken);

            return Unit.Task.Result;
        }

     
    }
}
