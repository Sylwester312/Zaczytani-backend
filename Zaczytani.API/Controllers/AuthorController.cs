﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Shared.Queries;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuthorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        var authors = await _mediator.Send(new GetAuthorsQuery());
        return Ok(authors);
    }

    [HttpPost("{authorId}/AddImage")]
    public async Task<ActionResult<Guid>> AddAuthorPhoto([FromRoute] Guid authorId, [FromBody] string fileName)
    {
        var command = new AddAuthorImageCommand
        {
            AuthorId = authorId,
            FileName = fileName
        };

        var result = await _mediator.Send(command);
        return NoContent();
    }
}
