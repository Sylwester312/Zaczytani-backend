using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Queries;
using Zaczytani.Application.Client.Commands;
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

    [HttpGet("Pending")]
    public async Task<ActionResult<IEnumerable<BookRequestDto>>> GetBookRequests()
    {
        var bookRequests = await _mediator.Send(new GetBookRequestsQuery());
        return Ok(bookRequests);
    }
}
