using Zaczytani.Domain.Entities;

public interface IUserDrawnBookRepository
{
    Task<UserDrawnBook?> GetDrawnBookByUserIdAndDateAsync(Guid userId, DateTime date, CancellationToken cancellationToken);
    Task AddAsync(UserDrawnBook userDrawnBook);
    Task SaveChangesAsync();
}
