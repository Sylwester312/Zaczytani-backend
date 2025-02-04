using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Application.Shared.Queries;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[SetUserId]
[Route("api/[controller]")]
public class BookController(IMediator mediator, ILogger<BookController> logger) : ControllerBase
{
    private readonly ILogger<BookController> _logger = logger;
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        var bookId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBookDetails), new { id = bookId }, new { id = bookId });
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
    {
        var command = new DeleteBookCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<SearchDto>>> SearchBook([FromQuery] SearchBookQuery command)
    {
        var books = await _mediator.Send(command);

        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookDto>> GetBookDetails(Guid id)
    {
        var query = new GetBookDetailsQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditBookDetails(Guid id, [FromBody] EditBookDetailsCommand command)
    {
        command.SetId(id);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{id:guid}/Reviews")]
    public async Task<ActionResult<IEnumerable<BookReviewDto>>> GetBookReviews(Guid id)
    {
        var query = new GetBookReviewsQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Genres")]
    public ActionResult<IEnumerable<BookGenre>> GetBookGenres()
    {
        var genres = EnumHelper.GetEnumDescriptions<BookGenre>();

        return Ok(genres);
    }

    [HttpGet("PublishingHouses")]
    public async Task<ActionResult<BookDto>> GetPublishingHouses()
    {
        var result = await _mediator.Send(new GetPublishingHousesQuery());
        return Ok(result);
    }

    [HttpPost("Random")]
    public async Task<ActionResult<BookDto>> GetRandomBook()
    {
        var command = new GetRandomBookCommand();
        var book = await _mediator.Send(command);
        return Ok(book);
    }

    [HttpGet("HasDrawn")]
    public async Task<ActionResult<BookDto>> HasDrawnBookToday()
    {
        var query = new HasDrawnBookTodayQuery();
        var book = await _mediator.Send(query);
        return Ok(book);
    }

    [HttpGet("CurrentlyReading")]
    public async Task<ActionResult<IEnumerable<ReadingBookDto>>> GetCurrentlyReadingBooks()
    {
        var query = new GetCurrentlyReadingBooksQuery();
        var books = await _mediator.Send(query);
        return Ok(books);
    }

}
