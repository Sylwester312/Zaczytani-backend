using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Filters;

namespace Zaczytani.API.Controllers;

[ApiController]
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
}
