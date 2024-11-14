namespace Zaczytani.Application.Dtos;

public record SearchDto(Guid Id, string Name, IEnumerable<SearchBookDto> Books);
public record SearchBookDto(Guid Id, string Title, string Isbn, string Description, int PageNumber);