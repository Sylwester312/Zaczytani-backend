using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IBookRequestRepository
{
    Task AddAsync(BookRequest entity);
    IQueryable<BookRequest> GetAllPending();
    IQueryable<BookRequest> GetByUserId(Guid userId);
    Task<BookRequest?> GetByIdAsync(Guid id);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}