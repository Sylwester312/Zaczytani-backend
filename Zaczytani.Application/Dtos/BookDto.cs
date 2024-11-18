namespace Zaczytani.Application.Dtos;

public record BookDto(Guid Id, string Title, string Isbn, string Description, string? ImageUrl, int PageNumber, IEnumerable<AuthorDto> Authors);

