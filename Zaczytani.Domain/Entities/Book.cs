namespace Zaczytani.Domain.Entities;    

public class Book
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Isbn { get; set; }
    public string? Description { get; set; }
    public int PageNumber { get; set; }

    // Kolekcja autorów jako relacja wiele-do-wielu
    public ICollection<Author> Authors { get; set; } = new List<Author>();
}
