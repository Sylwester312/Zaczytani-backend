namespace Zaczytani.Application.Dtos;

public record BookDto(
    Guid Id,
    string Title,
    string Isbn,
    string Description,
    int PageNumber,
    string Authors);

