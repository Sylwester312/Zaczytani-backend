using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public class GetBookShelfQuery : IRequest<BookDto>
{
    private class GetBookShelfQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetBookShelfQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public Task<BookDto> Handle(GetBookShelfQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
