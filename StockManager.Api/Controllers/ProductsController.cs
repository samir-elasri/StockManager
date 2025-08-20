using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Products.Commands;
using StockManager.Application.Products.Queries;

namespace StockManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return Ok(new { ProductId = id });
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
        {
            var items = await _mediator.Send(new GetProductsQuery(userId));
            return Ok(items);
        }

        // Optional: Update and Delete endpoints here
    }
}
