using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Categories.Commands;
using StockManager.Application.Categories.Queries;

namespace StockManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { CategoryId = id });
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser()
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var categories = await _mediator.Send(new GetCategoriesQuery(userId));
            return Ok(categories);
        }
    }
}
