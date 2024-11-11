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
        => _dbContext.Books.Where(b => b.Title.Contains(searchPhrase)).OrderBy(b => b.Title);
    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
