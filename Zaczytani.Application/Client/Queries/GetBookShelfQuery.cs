using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public class GetBookShelfQuery : IRequest<IEnumerable<BookDto>>
{
    private class GetBookShelfQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetBookShelfQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public Task<IEnumerable<BookDto>> Handle(GetBookShelfQuery request, CancellationToken cancellationToken)
        {
            var books = new List<BookDto>
            {
                new BookDto(Guid.NewGuid(), "Tytuł", "1234567890", "Opis książki", 300, "Autor 1, Autor 2")
            };

            return Task.FromResult(books.AsEnumerable());
        }
    }
}
