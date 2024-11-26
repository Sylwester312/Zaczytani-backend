﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Infrastructure.Persistance;

internal class BookDbContext(DbContextOptions options) : IdentityDbContext<User, UserRole, Guid>(options)
{
    internal DbSet<Book> Books { get; set; } = null!;
    internal DbSet<BookRequest> BookRequests { get; set; } = null!;
    internal DbSet<Author> Authors { get; set; } = null!;
    internal DbSet<PublishingHouse> PublishingHouses { get; set; } = null!;
    internal DbSet<UserBook> UserBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookRequest>()
            .Property(b => b.Status)
            .HasConversion<int>();
    }
}   
