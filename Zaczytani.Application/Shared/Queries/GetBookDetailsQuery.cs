using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;
using Zaczytani.Application.Exceptions;

namespace Zaczytani.Application.Shared.Queries;

public class GetBookDetailsQuery : IRequest<BookDto>
{
    public Guid BookId { get; }

    public GetBookDetailsQuery(Guid bookId)
    {
        BookId = bookId;
    }

    private class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookDetailsQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            }

            return _mapper.Map<BookDto>(book);
        }
    }
}
