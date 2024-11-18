using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public record GetBookDetailsQuery(Guid BookId) : IRequest<BookDto>
{
    private class GetBookDetailsQueryHandler(IBookRepository BookRepository, IMapper Mapper)
        : IRequestHandler<GetBookDetailsQuery, BookDto>
    {
        public async Task<BookDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
        {
            var book = await BookRepository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            }

            return Mapper.Map<BookDto>(book);
        }
    }
}
