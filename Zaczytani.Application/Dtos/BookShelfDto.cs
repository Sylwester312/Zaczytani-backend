﻿using System;

public class BookShelfDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; } 
    public bool IsDefault { get; set; }
}
