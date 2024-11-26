using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public class GetRandomBookQuery : IRequest<BookDto>
{
    private class GetRandomBookQueryHandler(IBookRepository bookRepository, IUserBookRepository userBookRepository, IUserContextService userContextService, IMapper mapper) : IRequestHandler<GetRandomBookQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUserBookRepository _userBookRepository = userBookRepository;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly IMapper _mapper = mapper;

        public async Task<BookDto> Handle(GetRandomBookQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            var drawnBookIds = await _userBookRepository.GetDrawnBookIdsByUserAsync(userId);

            var randomBook = await _bookRepository.GetAll()
                .Where(book => !drawnBookIds.Contains(book.Id))
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<BookDto>(randomBook);
        }
    }
}

