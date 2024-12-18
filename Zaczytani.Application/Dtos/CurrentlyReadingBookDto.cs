namespace Zaczytani.Application.Dtos;

public class CurrentlyReadingBookDto
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public IEnumerable<AuthorDto> Authors { get; set; }
    public int Progress { get; set; }
    public string? ImageUrl { get; set; }
}