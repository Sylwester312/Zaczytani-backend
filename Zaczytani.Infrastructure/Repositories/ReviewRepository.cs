using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class ReviewRepository(BookDbContext dbContext) : IReviewRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(Review entity) => await _dbContext.AddAsync(entity);
    public async Task<IEnumerable<Review>> GetCurrentlyReadingBooksAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(r => r.UserId == userId && !r.IsFinal)
            .Include(r => r.Book)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
