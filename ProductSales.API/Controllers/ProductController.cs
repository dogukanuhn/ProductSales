using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Application.Products.Commands;
using ProductSales.Application.Products.Queries;
using System.Threading.Tasks;

namespace ProductSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        public readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProductListQuery());



            return Ok(result);
        }


        [AllowAnonymous]
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromQuery] GetProductByNameQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]

        public async Task<IActionResult> Add([FromBody] AddProductCommand command)
        {
            await _mediator.Send(command);


            return Created("Created", string.Empty);
        }

        [HttpDelete]

        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            await _mediator.Publish(command);

            return Ok();
        }


        [HttpPut]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Updatea([FromBody] UpdateProductCommand command)
        {
            await _mediator.Publish(command);

            return Ok();
        }


    }
}
