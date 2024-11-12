using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Dtos;

public record BookDto(Guid Id, string Title, string Isbn, string Description, int PageNumber, IEnumerable<AuthorDto> Authors);

