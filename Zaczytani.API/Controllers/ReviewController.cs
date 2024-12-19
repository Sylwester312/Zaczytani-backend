using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
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

    [HttpGet("{bookId}/Progress")]
    public async Task<ActionResult<ReadingBookDto>> GetReadingBook(Guid bookId)
    {
        var query = new GetReadingBookDetailsQuery(bookId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("CurrentlyReading")]
    public async Task<ActionResult<IEnumerable<CurrentlyReadingBookDto>>> GetCurrentlyReadingBooks()
    {
        var query = new GetCurrentlyReadingBooksQuery();
        var books = await _mediator.Send(query);
        return Ok(books);
    }

    [HttpPost("{reviewId}/Comment")]
    public async Task<ActionResult> AddComment([FromRoute] Guid reviewId, [FromBody] CreateCommentCommand command)
    {
        command.SetReviewId(reviewId);
        await _mediator.Send(command);

        return NoContent();
    }

    //Temporary solutions
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetReviewDetails(Guid id)
    {
        return Ok();
    }
}

