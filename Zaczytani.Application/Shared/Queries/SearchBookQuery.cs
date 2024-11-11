using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public class SearchBookQuery : IRequest<IEnumerable<BookDto>>
{
    public string SearchPhrase { get; set; } = string.Empty;

    private class SearchBookQueryHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<SearchBookQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BookDto>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBySearchPhrase(request.SearchPhrase)
                .Include(b => b.Authors)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}
