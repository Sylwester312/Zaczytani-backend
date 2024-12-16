using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IReviewRepository
{
    Task AddAsync(Review entity);
    Task<Review?> GetReviewByIdAsync(Guid id);
    Task SaveChangesAsync();
}
