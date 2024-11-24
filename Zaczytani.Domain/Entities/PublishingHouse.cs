using System.ComponentModel.DataAnnotations;

namespace Zaczytani.Domain.Entities;

public class PublishingHouse
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
}
