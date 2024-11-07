using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Filters;

namespace Zaczytani.API.Controllers;

[ApiController]
[SetUserId]
[Route("api/[controller]")]
public class BookController(ILogger<BookController> logger) : ControllerBase
{
    private readonly ILogger<BookController> _logger = logger;


}
