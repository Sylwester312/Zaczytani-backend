using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zaczytani.Domain.Enums;

namespace Zaczytani.Domain.Entities;

public class BookRequest
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MinLength(10)]
    [MaxLength(13)]
    public string? Isbn { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int? PageNumber { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Image { get; set; }

    public string Authors { get; set; } = string.Empty;

    public string? PublishingHouse { get; set; }

    public List<BookGenre> Genre { get; set; } = [];

    public string? Series { get; set; }

    public DateTime CreatedDate { get; set; } = new DateTime();

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User User { get; set; } = new();

    public BookRequestStatus Status = BookRequestStatus.Pending;
}

