using MediatR;
using ProductSales.Application.Constants;
using ProductSales.Application.Payments.Commands;
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
    public class StartPaymentCommandHandler : IRequestHandler<StartPaymentCommand, Unit>

    {
        private readonly IPaymentService _paymentService;
        private readonly ISellerRepository _sellerRepository;
        

        public StartPaymentCommandHandler(IPaymentService paymentService, ISellerRepository sellerRepository)
        {
            _paymentService = paymentService;
            _sellerRepository = sellerRepository;
          
        }
        public async Task<Unit> Handle(StartPaymentCommand notification, CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetAsync(x => x.Code == notification.SellerCode);

            CustomerPayment customerPayment = new(notification.BasketCode, notification.Price, notification.PaidPrice, notification.CustomerCode,
                notification.BasketItems, notification.BillingAddress, notification.ShippingAddress, notification.PaymentCard, notification.IP, notification.SellerCode, seller.Notifications,seller.Email,seller.Phone);
            
            
            
            await _paymentService.Create(customerPayment);


            

 
            return Unit.Task.Result;

        }

        public class Message
        {
            public string MessageText { get; set; }
            public string To { get; set; }

        }
    }
}
