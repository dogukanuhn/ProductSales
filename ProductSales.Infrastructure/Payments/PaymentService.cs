using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Abstract.Services;
using ProductSales.Domain.Exceptions;
using ProductSales.Domain.Models;
using ProductSales.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {

        private readonly Iyzipay.Options _iyziOpt;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICipherService _cipherService;


        public PaymentService(IOptions<IyzicoSettings> options, ICustomerRepository customerRepository, ICipherService cipherService)
        {
            _iyziOpt = new Iyzipay.Options();
            _iyziOpt.ApiKey = options.Value.ApiKey;
            _iyziOpt.BaseUrl = options.Value.BaseUrl;
            _iyziOpt.SecretKey = options.Value.SecretKey;
            _customerRepository = customerRepository;
            _cipherService = cipherService;
        }

        public async Task Create(CustomerPayment customerPayment)
        {
            var customer = await _customerRepository.GetAsync(x => x.Code == customerPayment.CustomerCode);
            customer.IdentityNumber = _cipherService.Decrypt(customer.IdentityNumber);
            Iyzipay.Model.PaymentCard paymentCard = new()
            {
                CardHolderName = customerPayment.PaymentCard.CardHoldername,
                CardNumber = customerPayment.PaymentCard.CardNumber,
                Cvc = customerPayment.PaymentCard.Cvc,
                ExpireMonth = customerPayment.PaymentCard.ExpireMonth,
                ExpireYear = customerPayment.PaymentCard.ExpireYear,

            };

            Buyer buyer = new();
            buyer.Id = customer.Code.ToString();
            buyer.Ip = customerPayment.IP;
            buyer.Name = customer.Name;
            buyer.Surname = customer.Surname;
            buyer.Email = customer.Email;
            buyer.IdentityNumber = customer.IdentityNumber;
            buyer.City = customer.Address[0].City;
            buyer.RegistrationAddress = customer.Address[0].Description;
            buyer.Country = customer.Address[0].Country;
            buyer.ZipCode = customer.Address[0].ZipCode;

            Address shippingAddress = new();
            shippingAddress.ContactName = customerPayment.ShippingAddress.ContactName;
            shippingAddress.City = customerPayment.ShippingAddress.City;
            shippingAddress.Country = customerPayment.ShippingAddress.Country;
            shippingAddress.Description = customerPayment.ShippingAddress.Description;
            shippingAddress.ZipCode = customerPayment.ShippingAddress.ZipCode;

            List<Iyzipay.Model.BasketItem> basketItems = new List<Iyzipay.Model.BasketItem>();
            customerPayment.BasketItems.ForEach(x =>
            {
                basketItems.Add(new Iyzipay.Model.BasketItem()
                {
                    Category1 = x.Category1,
         
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = x.Price.ToString().ChangeDecimal(),
                    Id = x.Sku.ToString(),
                    Name = x.Name

                });
            });

            decimal totalPrice = customerPayment.BasketItems.Sum(x => x.Price);
            CreatePaymentRequest request = new()
            {
                PaymentCard = paymentCard,
                Buyer = buyer,
                ShippingAddress = shippingAddress,
                BillingAddress = shippingAddress,
                BasketItems = basketItems,
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                Price = totalPrice.ToString().ChangeDecimal(),
                PaidPrice = (totalPrice *= (decimal)1.18).ToString().ChangeDecimal()
            };

            Payment payment = Payment.Create(request, _iyziOpt);

            if (payment.Status == "failure")
            {
                throw new PaymentFailureException(payment.ErrorMessage);
            }
        }


      
    }

     static class ChangeExtension
    {
        public static string ChangeDecimal(this string str)
        {
            return str.Replace(",", ".");
        }
    }



}
