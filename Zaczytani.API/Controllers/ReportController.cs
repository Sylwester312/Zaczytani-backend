using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Filters;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[SetUserId]
[Route("api/[controller]")]
public class ReportController(IMediator mediator) : ControllerBase
{

    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult> ReportUser([FromBody] ReportUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
