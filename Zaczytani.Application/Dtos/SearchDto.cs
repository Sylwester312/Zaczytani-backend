namespace Zaczytani.Application.Dtos;

public record SearchDto(Guid Id, string Name, string? ImageUrl, IEnumerable<BookDto> Books);
