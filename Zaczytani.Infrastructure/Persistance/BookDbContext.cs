using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Infrastructure.Persistance;

internal class BookDbContext(DbContextOptions options) : IdentityDbContext<User, UserRole, Guid>(options)
{
    internal DbSet<Book> Books { get; set; } = null!;
    internal DbSet<BookRequest> BookRequests { get; set; } = null!;
    internal DbSet<Author> Authors { get; set; } = null!;
}
