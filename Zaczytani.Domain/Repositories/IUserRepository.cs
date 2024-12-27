using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;

namespace Zaczytani.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<BookGenre>> GetFavoriteGenresAsync(Guid userId, CancellationToken cancellationToken);
    }
}
