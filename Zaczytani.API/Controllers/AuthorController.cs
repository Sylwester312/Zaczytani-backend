using MediatR;
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

    [HttpPost("AddImage")]
    public async Task<ActionResult<Guid>> AddAuthorPhoto([FromBody] AddAuthorImageCommand command)
    {
        var result = await _mediator.Send(command);
        return NoContent();
    }
}
