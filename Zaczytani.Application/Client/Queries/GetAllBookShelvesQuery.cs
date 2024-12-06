using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;
public class GetAllBookshelvesQuery : IRequest<IEnumerable<BookShelfDto>>, IUserIdAssignable
{
    private Guid UserId { get; set; }

    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(IBookShelfRepository bookshelfRepository, IMapper mapper) : IRequestHandler<GetAllBookshelvesQuery, IEnumerable<BookShelfDto>>
    {
        private readonly IBookShelfRepository _bookshelfRepository = bookshelfRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BookShelfDto>> Handle(GetAllBookshelvesQuery request, CancellationToken cancellationToken)
        {
            var bookshelves = await _bookshelfRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);
            return _mapper.Map<IEnumerable<BookShelfDto>>(bookshelves);
        }
    }
}
