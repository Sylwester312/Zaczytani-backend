using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IBookRequestRepository
{
    Task AddAsync(BookRequest entity);
    Task SaveChangesAsync();
}