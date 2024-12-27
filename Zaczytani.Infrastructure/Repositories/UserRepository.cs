using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class UserRepository(BookDbContext dbContext) : IUserRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(u => u.FavoriteGenres)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
    public async Task<List<BookGenre>> GetFavoriteGenresAsync(Guid userId, CancellationToken cancellationToken)
    {
        var books = await _dbContext.BookShelves
            .Where(bs => bs.UserId == userId && bs.Type == BookShelfType.Read)
            .SelectMany(bs => bs.Books)
            .ToListAsync(cancellationToken);

        return books.SelectMany(b => b.Genre)
            .GroupBy(g => g)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key)
            .ToList();
    }
}
