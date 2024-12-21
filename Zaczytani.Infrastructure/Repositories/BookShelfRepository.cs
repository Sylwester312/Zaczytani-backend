using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Enums;

namespace Zaczytani.Infrastructure.Repositories;
internal class BookShelfRepository(BookDbContext dbContext) : IBookShelfRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task<BookShelf?> GetBookShelfByTypeAsync(BookShelfType type, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.BookShelves
            .Include(b => b.Books)
                .ThenInclude(b => b.Authors)
            .Where(b => b.Type == type && b.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<BookShelf>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.BookShelves
            .Where(b => b.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<BookShelf?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BookShelves
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
    public async Task<BookShelf?> GetByIdWithBooksAsync(Guid shelfId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.BookShelves
            .Include(bs => bs.Books)
            .FirstOrDefaultAsync(bs => bs.Id == shelfId && bs.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(BookShelf bookshelf, CancellationToken cancellationToken)
    {
        await _dbContext.BookShelves.AddAsync(bookshelf, cancellationToken);
    }

    public async Task UpdateAsync(BookShelf bookshelf, CancellationToken cancellationToken)
    {
        _dbContext.BookShelves.Update(bookshelf);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var shelf = await GetByIdAsync(id, cancellationToken);
        if (shelf != null)
        {
            _dbContext.BookShelves.Remove(shelf);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

