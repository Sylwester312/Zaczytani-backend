﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;

[ApiController]
[Authorize]
[SetUserId]
[Route("api/[controller]")]
public class BookshelfController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookshelfController(IMediator mediator) => _mediator = mediator;

    [HttpPost("Create")]
    public async Task<ActionResult> CreateShelf([FromBody] CreateBookShelfCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetShelf), new { id }, new { Id = id });
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateShelf([FromBody] UpdateBookShelfCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteShelf([FromBody] DeleteBookShelfCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("GetBookshelf/{id:guid}")]
    public async Task<ActionResult<BookShelfDto>> GetShelf(Guid id)
    {
        var query = new GetBookShelfQuery(id);
        var shelf = await _mediator.Send(query);
        return shelf == null ? NotFound() : Ok(shelf);
    }

    [HttpGet("GetAllBookShelves")]
    public async Task<ActionResult<IEnumerable<BookShelfDto>>> GetAllShelves()
    {
        var query = new GetAllBookshelvesQuery();
        var shelves = await _mediator.Send(query);
        return Ok(shelves);
    }

    [HttpGet("{id}/Books")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksOnShelf(Guid id)
    {
        var query = new GetBooksOnShelfQuery(id);
        var books = await _mediator.Send(query);
        return Ok(books);
    }

}
