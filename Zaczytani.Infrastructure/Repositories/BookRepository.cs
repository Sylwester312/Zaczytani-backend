using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class BookRepository(BookDbContext dbContext) : IBookRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(Book entity) => await _dbContext.AddAsync(entity);
    
    public IQueryable<Book> GetBySearchPhrase(string searchPhrase)
        => _dbContext.Books.Where(b => b.Title.Contains(searchPhrase) 
                                    || b.Isbn.Contains(searchPhrase) 
                                    || b.Authors.Any(a => a.Name.Contains(searchPhrase)))
        .OrderBy(b => b.Title);

    public async Task<Book?> GetByIdAsync(Guid bookId)
    {
        return await _dbContext.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == bookId);
    }
    
    public async Task<Author?> GetAuthorByIdAsync(Guid authorId)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
    }
    
    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
