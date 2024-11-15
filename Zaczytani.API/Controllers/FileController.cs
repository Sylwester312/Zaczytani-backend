using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FileController(IFileStorageRepository fileStorageRepository) : ControllerBase
{
    private readonly IFileStorageRepository _fileStorageRepository = fileStorageRepository;

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetFile([FromRoute] string fileName)
    {
        var fileBytes = await _fileStorageRepository.GetFileAsync(fileName);

        return fileBytes is null ? NotFound() : File(fileBytes, "image/jpeg");
    }
}
