using MediatR;
using AutoMapper;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;

public record HasDrawnBookTodayQuery : IRequest<BookDto?>, IUserIdAssignable
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class Handler(IUserDrawnBookRepository userDrawnBookRepository, IMapper mapper) : IRequestHandler<HasDrawnBookTodayQuery, BookDto?>
    {
        private readonly IUserDrawnBookRepository _userDrawnBookRepository = userDrawnBookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookDto?> Handle(HasDrawnBookTodayQuery request, CancellationToken cancellationToken)
        {
            var drawnBook = await _userDrawnBookRepository.GetDrawnBookByUserIdAndDateAsync(request.UserId, DateTime.UtcNow.Date, cancellationToken);

            if (drawnBook == null)
            {
                return null;
            }

            return _mapper.Map<BookDto>(drawnBook.Book);
        }
    }
}
