using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid authorId);
    Task SaveChangesAsync();
}
