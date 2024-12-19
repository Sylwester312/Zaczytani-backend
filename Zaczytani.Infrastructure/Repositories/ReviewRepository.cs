using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class ReviewRepository(BookDbContext dbContext) : IReviewRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(Review entity, CancellationToken cancellationToken) => await _dbContext.AddAsync(entity, cancellationToken);

    public async Task AddCommentAsync(Comment entity, CancellationToken cancellationToken) => await _dbContext.AddAsync(entity, cancellationToken);

    public async Task<IEnumerable<Review>> GetCurrentlyReadingBooksAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(r => r.UserId == userId && !r.IsFinal)
            .Include(r => r.Book)
            .ThenInclude(b => b.Authors)
            .ToListAsync(cancellationToken);
    }

    public async Task<Review?> GetLatestReviewByBookIdAsync(Guid bookId, Guid userId, CancellationToken cancellationToken)
        => await _dbContext.Reviews
        .Include(r => r.Book)
        .Where(r => r.BookId == bookId && r.UserId == userId)
        .OrderByDescending(r => r.CreatedDate)
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken) => await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) => _dbContext.SaveChangesAsync(cancellationToken);
}
