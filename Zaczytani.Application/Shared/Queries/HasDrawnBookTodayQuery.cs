using MediatR;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public class HasDrawnBookTodayQuery : IRequest<bool>
{
    private class HasDrawnBookTodayQueryHandler(IUserBookRepository userBookRepository, IUserContextService userContextService) : IRequestHandler<HasDrawnBookTodayQuery, bool>
    {
        private readonly IUserBookRepository _userBookRepository = userBookRepository;
        private readonly IUserContextService _userContextService = userContextService;

        public async Task<bool> Handle(HasDrawnBookTodayQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            var hasDrawn = await _userBookRepository.HasUserDrawnBookTodayAsync(userId);
            return hasDrawn;
        }
    }
}
