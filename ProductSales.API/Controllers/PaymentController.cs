using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Application.Helpers;
using ProductSales.Application.Payments.Events;
using System;
using System.Threading.Tasks;

namespace ProductSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Payment([FromBody] CreatePaymentNotification notification)
        {
            notification.CustomerCode = Guid.Parse(HttpContext.User.FindFirst(JwtClaims.UserCode.ToString()).Value);
            notification.IP = HttpContext.Connection.RemoteIpAddress.ToString();
            await _mediator.Publish(notification);

            return Accepted("Payment Accepted", string.Empty);
        }
    }
}
