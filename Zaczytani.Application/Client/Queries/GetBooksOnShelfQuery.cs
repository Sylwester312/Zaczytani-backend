using AutoMapper;
using MediatR;
using Zaczytani.Domain.Repositories;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;

namespace Zaczytani.Application.Client.Queries;

public record GetBooksOnShelfQuery(Guid Id) : IRequest<IEnumerable<BookDto>>, IUserIdAssignable
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(IBookShelfRepository bookshelfRepository, IMapper mapper) : IRequestHandler<GetBooksOnShelfQuery, IEnumerable<BookDto>>
    {
        private readonly IBookShelfRepository _bookshelfRepository = bookshelfRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BookDto>> Handle(GetBooksOnShelfQuery request, CancellationToken cancellationToken)
        {
            var bookshelf = await _bookshelfRepository.GetByIdWithBooksAsync(request.Id, request.UserId, cancellationToken);

            if (bookshelf == null)
                throw new KeyNotFoundException("Bookshelf not found or access denied.");

            return _mapper.Map<IEnumerable<BookDto>>(bookshelf.Books);
        }
    }
}
