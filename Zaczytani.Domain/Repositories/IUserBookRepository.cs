public interface IUserBookRepository
{
    Task<bool> HasUserDrawnBookTodayAsync(Guid userId);
}
