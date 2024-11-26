public interface IUserBookRepository
{
    Task<bool> HasUserDrawnBookTodayAsync(Guid userId);
    Task<List<Guid>> GetDrawnBookIdsByUserAsync(Guid userId);
}

