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
public class ReviewController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("{bookId}")]
    public async Task<ActionResult> AddReview([FromRoute] Guid bookId, [FromBody] CreateReviewCommand command)
    {
        command.SetBookId(bookId);
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetReviewDetails), new { id }, new { id });
    }

    //Temporary solutions
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetReviewDetails(Guid id)
    {
        return Ok();
    }
}
