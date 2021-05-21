using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Application.Helpers;
using ProductSales.Application.Sellers.Commands.Product;
using System.Threading.Tasks;

namespace ProductSales.API.Controllers
{
    [Route("api/seller")]
    [ApiController]
    [Authorize]
    public class SellerController : ControllerBase
    {
        public readonly IMediator _mediator;

        public SellerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var code = HttpContext.User.FindFirst(JwtClaims.UserCode.ToString()).Value;
        //    //GetSellerQuery query = new 

        //    return Ok();
        //}

        [HttpPost("product/add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddSellerProduct([FromBody] AddProductToSellerCommand command)
        {
            command.SellerCode = HttpContext.User.FindFirst(JwtClaims.UserCode.ToString()).Value;
            await _mediator.Send(command);

            return Created("Product Created", string.Empty);
        }


    }
}
