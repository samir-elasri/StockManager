using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Users.Commands;

namespace StockManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { UserId = id });
        }
    }
}
