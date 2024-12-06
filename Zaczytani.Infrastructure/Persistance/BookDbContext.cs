using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Infrastructure.Persistance;

internal class BookDbContext(DbContextOptions options) : IdentityDbContext<User, UserRole, Guid>(options)
{
    internal DbSet<Book> Books { get; set; } = null!;
    internal DbSet<BookRequest> BookRequests { get; set; } = null!;
    internal DbSet<Author> Authors { get; set; } = null!;
    internal DbSet<PublishingHouse> PublishingHouses { get; set; } = null!;
    internal DbSet<UserDrawnBook> UserBooks { get; set; }
    internal DbSet<BookShelf> BookShelves { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookRequest>()
            .Property(b => b.Status)
            .HasConversion<int>();

        modelBuilder.Entity<Book>()
                .Property(b => b.Rating)
                .HasPrecision(5, 2);

        modelBuilder.Entity<UserDrawnBook>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBooks)
            .HasForeignKey(ub => ub.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserDrawnBook>()
            .HasOne(ub => ub.Book)
            .WithMany()
            .HasForeignKey(ub => ub.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookShelf>()
            .HasOne(bs => bs.User)
            .WithMany(u => u.BookShelves)
            .HasForeignKey(bs => bs.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}   
