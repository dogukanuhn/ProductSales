using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Application.Customers.Commands;
using ProductSales.Application.Customers.Queries;
using ProductSales.Application.Sellers.Commands;
using ProductSales.Application.Sellers.Queries;
using System.Threading.Tasks;

namespace ProductSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region SELLER
        [AllowAnonymous]
        [HttpPost("seller/login")]
        public async Task<IActionResult> SellerLogin([FromBody] LoginSellerQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("seller/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SellerRegister([FromBody] RegisterSellerCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("Created", result);
        }

        #endregion


        #region SELLER

        [AllowAnonymous]
        [HttpPost("customer/login")]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginCustomerQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("customer/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CustomerRegister([FromBody] RegisterCustomerCommand command)
        {
            await _mediator.Send(command);

            return Created("Register", string.Empty);
        }

        #endregion
    }
}
