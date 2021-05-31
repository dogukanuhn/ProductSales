using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSales.API.Dtos;
using ProductSales.Application.Customers.Commands;

using ProductSales.Application.Sellers.Commands;
using ProductSales.Application.Services;
using System.Threading.Tasks;

namespace ProductSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticationService _auth;
        public AuthController(IMediator mediator, IAuthenticationService auth)
        {
            _mediator = mediator;
            _auth = auth;
        }

        #region SELLER

        [HttpPost("seller/login")]
        public async Task<IActionResult> SellerLogin([FromBody] LoginDTO dto)
        {
            var result = await _auth.LoginSeller(dto.Email, dto.Password);

            return Ok(result);
        }


        [HttpPost("seller/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SellerRegister([FromBody] RegisterSellerCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("Created", result);
        }

        #endregion


        #region CUSTOMER


        [HttpPost("customer/login")]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginDTO dto)
        {
            var result = await _auth.LoginCustomer(dto.Email, dto.Password);
            return Ok(result);
        }

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
