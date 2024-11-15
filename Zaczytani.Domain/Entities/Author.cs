using System.ComponentModel.DataAnnotations;

namespace Zaczytani.Domain.Entities;

public class Author
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    public string? Image { get; set; }

    public List<Book> Books { get; set; } = [];
}
