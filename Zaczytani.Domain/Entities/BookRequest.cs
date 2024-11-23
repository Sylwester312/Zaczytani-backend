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
    public string? Isbn { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; } = string.Empty;

    public int? PageNumber { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Image { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public Guid CreatedById { get; set; }

    public User CreatedBy { get; set; } = new();

    public BookRequestStatus Status = BookRequestStatus.Pending;

    public string Authors { get; set; } = string.Empty;
}
