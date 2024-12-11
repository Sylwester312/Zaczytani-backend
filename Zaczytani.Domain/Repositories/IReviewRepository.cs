using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IReviewRepository
{
    Task AddAsync(Review entity);
    Task<IEnumerable<Review>> GetCurrentlyReadingBooksAsync(Guid userId, CancellationToken cancellationToken);
    Task SaveChangesAsync();
}