using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Infrastructure.Persistance;

internal class BookDbContext(DbContextOptions options) : IdentityDbContext<User, UserRole, Guid>(options)
{
    internal DbSet<Book> Books { get; set; }
    internal DbSet<Author> Authors { get; set; }
}
