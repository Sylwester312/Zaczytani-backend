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

    public async Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        return await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.PublishingHouse)
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
    }

    public IQueryable<Book> GetUnseenBooks(Guid userId)
    {
        return _dbContext.Books
            .Where(b => !_dbContext.UserDrawnBook
                .Any(ub => ub.BookId == b.Id && ub.UserId == userId && ub.IsRead));
    }
    public async Task DeleteAsync(Book entity, CancellationToken cancellationToken)=> _dbContext.Remove(entity);
    public Task SaveChangesAsync(CancellationToken cancellationToken) => _dbContext.SaveChangesAsync(cancellationToken);
}
