namespace Zaczytani.Application.Dtos;

public class CurrentlyReadingBookDto
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Progress { get; set; }
    public string Image { get; set; } = string.Empty;
}