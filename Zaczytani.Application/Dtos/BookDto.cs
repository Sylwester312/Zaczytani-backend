﻿using Zaczytani.Domain.Enums;

namespace Zaczytani.Application.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public string Description { get; set; }
    public int PageNumber { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? ImageUrl { get; set; }
    public List<BookGenre> Genre { get; set; }
    public decimal Rating { get; set; }
    public string? Series { get; set; }
    public string PublishingHouse { get; set; }
    public IEnumerable<AuthorDto> Authors { get; set; }
}

