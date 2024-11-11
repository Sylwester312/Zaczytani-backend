using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Application.Shared.Queries;

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
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBook()
    {
        var result = await _mediator.Send(new GetBookShelfQuery());
        return Ok(result);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<BookDto>>> SearchBook([FromQuery] SearchBookQuery command)
    {
        var books = await _mediator.Send(command);

        return Ok(books);
    }
}
