using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Admin.Queries;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[SetUserId]
[Route("api/[controller]")]
public class BookRequestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateBookRequest([FromBody] CreateBookRequestCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPost("Accept/{id}")]
    public async Task<IActionResult> AcceptBookRequest([FromRoute] Guid id, [FromBody] AcceptBookRequestCommand command)
    {
        command.SetId(id);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBookRequestDto>>> GetUsersBookRequests()
    {
        var bookRequests = await _mediator.Send(new GetUsersBookRequestsQuery());
        return Ok(bookRequests);
    }

    [HttpGet("Pending")]
    public async Task<ActionResult<IEnumerable<BookRequestDto>>> GetPendingBookRequests()
    {
        var bookRequests = await _mediator.Send(new GetBookRequestsQuery());
        return Ok(bookRequests);
    }
}
