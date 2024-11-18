using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;
using Zaczytani.Application.Exceptions;

namespace Zaczytani.Application.Shared.Queries;

public class GetBookDetailsQuery(Guid BookId) : IRequest<BookDto>
{
    public Guid BookId { get; }

    private class GetBookDetailsQueryHandler(IBookRepository BookRepository, IMapper Mapper)
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

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
