using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManager.Application.ProductVariants.Commands;
using StockManager.Application.ProductVariants.Queries;

namespace StockManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VariantsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVariantCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { VariantId = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVariantCommand command)
        {
            if (id != command.Id) return BadRequest("Mismatched ID");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteVariantCommand(id));
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var variants = await _mediator.Send(new GetVariantsQuery(userId));
            return Ok(variants);
        }
    }
}
