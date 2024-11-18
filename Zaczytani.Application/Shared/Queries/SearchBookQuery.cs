using MediatR;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public class SearchBookQuery : IRequest<IEnumerable<SearchDto>>
{
    public string SearchPhrase { get; set; } = string.Empty;

    private class SearchBookQueryHandler(IBookRepository bookRepository) : IRequestHandler<SearchBookQuery, IEnumerable<SearchDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<IEnumerable<SearchDto>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBySearchPhrase(request.SearchPhrase)
                .Include(b => b.Authors)
                .ToListAsync(cancellationToken);

            var result = books
                .SelectMany(b => b.Authors.Select(a => new { Author = a, Book = b }))
                .GroupBy(x => x.Author)
                .Select(g => new SearchDto(
                    g.Key.Id,
                    g.Key.Name,
                    g.Select(x => new SearchBookDto(
                        x.Book.Id,
                        x.Book.Title,
                        x.Book.Isbn,
                        x.Book.Description,
                        x.Book.PageNumber))
                ));

            return result;
        }
    }
}
