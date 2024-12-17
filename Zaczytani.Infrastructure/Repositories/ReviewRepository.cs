using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class ReviewRepository(BookDbContext dbContext) : IReviewRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(Review entity) => await _dbContext.AddAsync(entity);

    public async Task<Review?> GetLatestReviewByBookIdAsync(Guid bookId, Guid userId, CancellationToken cancellationToken)
        => await _dbContext.Reviews
        .Include(r => r.Book)
        .Where(r => r.BookId == bookId && r.UserId == userId)
        .OrderByDescending(r => r.CreatedDate)
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<Review?> GetReviewByIdAsync(Guid id) => await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
