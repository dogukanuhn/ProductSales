using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Application.Helpers;
using ProductSales.Application.Payments;
using ProductSales.Application.Payments.Commands;
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
        public async Task<IActionResult> Payment([FromBody] StartPaymentCommand command)
        {
            command.CustomerCode = Guid.Parse(HttpContext.User.FindFirst(JwtClaims.UserCode.ToString()).Value);
            command.IP = HttpContext.Connection.RemoteIpAddress.ToString();
            await _mediator.Send(command);

            return Accepted("Payment Accepted", string.Empty);
        }
    }
}
