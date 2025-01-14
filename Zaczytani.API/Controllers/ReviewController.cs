﻿using MediatR;
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

    [HttpPost("{reviewId}/Comment")]
    public async Task<ActionResult> AddComment([FromRoute] Guid reviewId, [FromBody] CreateCommentCommand command)
    {
        command.SetReviewId(reviewId);
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReviewDetailsDto>> GetReviewDetails(Guid id)
    {
        var query = new GetReviewDetailsQuery(id);
        var reviewDetails = await _mediator.Send(query);
        return Ok(reviewDetails);
    }

    [HttpPost("{id:guid}/like")]
    public async Task<IActionResult> LikeReview([FromRoute] Guid id)
    {
        var command = new LikeReviewCommand(id);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("{id:guid}/unlike")]
    public async Task<IActionResult> UnlikeReview([FromRoute] Guid id)
    {
        var command = new UnlikeReviewCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
}

