﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Zaczytani.Domain.Enums;

namespace Zaczytani.Domain.Entities;

public class BookShelf
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;


    public bool IsDefault { get; set; } = false;

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public BookShelfType Type { get; set; } = BookShelfType.Other;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = [];
}
