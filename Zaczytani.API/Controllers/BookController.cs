using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Application.Shared.Queries;
using Zaczytani.Domain.Enums;

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

    [HttpGet]
    public async Task<IActionResult> GetBook()
    {
        var result = await _mediator.Send(new GetBookShelfQuery());
        return Ok(result);
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

    [HttpGet("Genres")]
    public ActionResult<IEnumerable<BookGenre>> GetBookGenres()
    {
        var genres = Enum.GetValues(typeof(BookGenre))
                          .Cast<BookGenre>()
                          .Select(g => g.ToString())
                          .ToList();

        return Ok(genres);
    }

    [HttpGet("PublishingHouses")]
    public async Task<ActionResult<BookDto>> GetPublishingHouses()
    {
        var result = await _mediator.Send(new GetPublishingHousesQuery());
        return Ok(result);
    }
}
