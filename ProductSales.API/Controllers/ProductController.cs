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

    public class ProductController : ControllerBase
    {
        public readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Domain.Abstract.IDataResult<System.Collections.Generic.List<Domain.Models.Product>>), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProductListQuery());



            return Ok(result);
        }


        [HttpGet("getbyname")]
        public async Task<IActionResult> GetByName([FromQuery] GetProductByNameQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddProductCommand command)
        {
            await _mediator.Send(command);


            return Created("Created", string.Empty);
        }



    }
}
