using System.ComponentModel.DataAnnotations;

namespace Zaczytani.Domain.Entities;

public class Book
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(13)]
    public string Isbn { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description {  get; set; } = string.Empty;

    [Required]
    public int PageNumber { get; set; }

    public string? Image { get; set; }

    /// <summary>
    /// Admin who created the book.
    /// Need for future support and debugging.
    /// </summary>
    public User? CreatedBy { get; set; }

    public List<Author> Authors { get; set; } = [];
}
