using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Constants;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Infrastructure.Persistance;
using static System.Reflection.Metadata.BlobBuilder;

namespace Zaczytani.Infrastructure.Seeders;

internal class Seeder(BookDbContext dbContext, IPasswordHasher<User> passwordHasher) : ISeeder
{
    private readonly BookDbContext _dbContext = dbContext;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Users.Any())
            {
                var users = GetUsers();
                await _dbContext.Users.AddRangeAsync(users);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.UserRoles.Any())
            {
                var userRoles = await GetUserRoles();
                await _dbContext.UserRoles.AddRangeAsync(userRoles);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Authors.Any())
            {
                var authors = GetAuthors();
                await _dbContext.Authors.AddRangeAsync(authors);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Books.Any())
            {
                var books = GetBooks();
                await _dbContext.Books.AddRangeAsync(books);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.BookRequests.Any())
            {
                var bookRequests = GetBookRequests();
                await _dbContext.BookRequests.AddRangeAsync(bookRequests);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.BookShelves.Any())
            {
                var bookShelves = GetDefaultBookShelves();
                await _dbContext.BookShelves.AddRangeAsync(bookShelves);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task<IEnumerable<IdentityUserRole<Guid>>> GetUserRoles()
    {
        var roles = await _dbContext.Roles.ToListAsync();
        var users = await _dbContext.Users.ToListAsync();

        List<IdentityUserRole<Guid>> userRoles = [
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.Admin).Id,
                UserId = users.First(u => u.Email == "admin@email.com").Id
            },
            new()
            {
                RoleId = roles.First(r => r.Name == UserRoles.User).Id,
                UserId = users.First(u => u.Email == "user@email.com").Id
            }
            ];

        return userRoles;
    }

    private IEnumerable<User> GetUsers()
    {
        var admin = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            Email = "admin@email.com",
            NormalizedEmail = "admin@email.com".ToUpper(),
            UserName = "admin",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "admin".ToUpper(),
        };

        admin.PasswordHash = _passwordHasher.HashPassword(admin, "password");

        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "User",
            Email = "user@email.com",
            NormalizedEmail = "user@email.com".ToUpper(),
            UserName = "user",
            SecurityStamp = Guid.NewGuid().ToString(),
            NormalizedUserName = "user".ToUpper(),
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, "password");

        return [admin, user];
    }

    private static List<UserRole> GetRoles()
    {
        List<UserRole> roles = [
            new(UserRoles.User)
            {
                Id = Guid.NewGuid(),
                NormalizedName = UserRoles.User,
            },
            new(UserRoles.Admin)
            {
                Id = Guid.NewGuid(),
                NormalizedName = UserRoles.Admin,
            }
            ];

        return roles;
    }


    private static List<Author> GetAuthors()
    {
        List<Author> authors = [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "J.K. Rowling"
            }
            ];

        return authors;
    }

    private List<Book> GetBooks()
    {
        List<Book> books = [
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter i Kamień Filozoficzny",
                Description = "Pierwszy tom cyklu \"Harry Potter\" w poważnej, \"dorosłej\" okładce. Harry Potter, sierota i podrzutek, od niemowlęcia wychowywany był przez ciotkę i wuja, którzy traktowali go jak piąte koło u wozu. Pochodzenie chłopca owiane jest tajemnicą; jedyną pamiątką Harry`ego z przeszłości jest zagadkowa blizna na czole. Skąd jednak biorą się niesamowite zjawiska, które towarzyszą nieświadomemu niczego Potterowi? Harry nigdy by się nie spodziewał, że można latać na miotle, znać bardzo pożyteczne zaklęcia i nosić pelerynę niewidkę. Nigdy też nie przyszłoby mu do głowy, że to właśnie on stoczy walkę z potężnym i złym Lordem Voldermortem.",
                Authors = [_dbContext.Authors.FirstOrDefault(a => a.Name.Contains("J.K"))],
                Genre = [BookGenre.Romance],
                Isbn = "1231231231",
                PageNumber = 10,
                ReleaseDate = new DateOnly(2010, 1, 1),
                Series = "Harry Potter",
                PublishingHouse = new PublishingHouse()
                {
                    Id = Guid.NewGuid(),
                    Name = "Media Rodzina"
                },
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter i Komnata Tajemnic",
                Description = "Drugi tom bestsellerowego cyklu w poważnej, \"dorosłej\" okładce.\r\nTym razem Harry będzie musiał się zmierzyć z tajemniczym potworem z komnaty tajemnic na zamku Hogwart. Otworzyć tę komnatę mógł jedynie prawowity dziedzic Slytherina, a wskutek nieszczęśliwego zbiegu okoliczności podejrzenie pada na Harryego. W dodatku jeden z najbliższych przyjaciół bohatera znajduje się w śmiertelnym niebezpieczeństwie. Z tego tomu dowiecie się, jaka była przeszłość Hagrida, jakie sekrety skrywa rodzina Malfoya i kto naprawdę wypuścił potwora.",
                Authors = [_dbContext.Authors.FirstOrDefault(a => a.Name.Contains("J.K"))],
                Genre= [BookGenre.Romance, BookGenre.Memoir],
                Isbn = "1231231231",
                PageNumber = 10,
                ReleaseDate = new DateOnly(2010, 1, 1),
                Series = "Harry Potter",
                PublishingHouse = new PublishingHouse()
                {
                    Id = Guid.NewGuid(),
                    Name = "Bloomsbury"
                },
            }
            ];

        return books;

    }
    private List<BookRequest> GetBookRequests()
    {
        List<BookRequest> bookRequests = [
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter i Kamień Filozoficzny",
                Description = "Pierwszy tom cyklu \"Harry Potter\" w poważnej, \"dorosłej\" okładce. Harry Potter, sierota i podrzutek, od niemowlęcia wychowywany był przez ciotkę i wuja, którzy traktowali go jak piąte koło u wozu. Pochodzenie chłopca owiane jest tajemnicą; jedyną pamiątką Harry`ego z przeszłości jest zagadkowa blizna na czole. Skąd jednak biorą się niesamowite zjawiska, które towarzyszą nieświadomemu niczego Potterowi? Harry nigdy by się nie spodziewał, że można latać na miotle, znać bardzo pożyteczne zaklęcia i nosić pelerynę niewidkę. Nigdy też nie przyszłoby mu do głowy, że to właśnie on stoczy walkę z potężnym i złym Lordem Voldermortem.",
                Authors = "J. K. Rowling",
                Genre = [BookGenre.Romance],
                Isbn = "1231231231",
                PageNumber = 10,
                ReleaseDate = new DateOnly(2010, 1, 1),
                Series = "Harry Potter",
                PublishingHouse = "Media Rodzina",
                User = _dbContext.Users.FirstOrDefault(u => u.Email == "user@email.com")!,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter i Komnata Tajemnic",
                Description = "Drugi tom bestsellerowego cyklu w poważnej, \"dorosłej\" okładce.\r\nTym razem Harry będzie musiał się zmierzyć z tajemniczym potworem z komnaty tajemnic na zamku Hogwart. Otworzyć tę komnatę mógł jedynie prawowity dziedzic Slytherina, a wskutek nieszczęśliwego zbiegu okoliczności podejrzenie pada na Harryego. W dodatku jeden z najbliższych przyjaciół bohatera znajduje się w śmiertelnym niebezpieczeństwie. Z tego tomu dowiecie się, jaka była przeszłość Hagrida, jakie sekrety skrywa rodzina Malfoya i kto naprawdę wypuścił potwora.",
                Authors = "Nie wiem",
                Genre= [BookGenre.Romance, BookGenre.Memoir],
                Isbn = "1231231231",
                ReleaseDate = new DateOnly(2010, 1, 1),
                Series = "Harry Potter",
                User = _dbContext.Users.FirstOrDefault(u => u.Email == "user@email.com")!,
            }
            ];

        return bookRequests;
    }

    private IEnumerable<BookShelf> GetDefaultBookShelves()
    {
        var books = GetBooks().ToList();

        return new List<BookShelf>
    {
        new BookShelf
        {
            Id = Guid.NewGuid(),
            Name = "Przeczytane",
            Description = "Lista przeczytanych książek",
            UserId = _dbContext.Users.FirstOrDefault(u => u.Email == "user@email.com")!.Id,
            Type = BookShelfType.Read,
            IsDefault = true,
            Books = new List<Book> { books.FirstOrDefault(b => b.Title.Contains("Harry Potter i Komnata Tajemnic"))! }

        },
        new BookShelf
        {
            Id = Guid.NewGuid(),
            Name = "Chce przeczytać",
            Description = "Lista książek do przeczytania",
            UserId = _dbContext.Users.FirstOrDefault(u => u.Email == "user@email.com")!.Id,
            Type = BookShelfType.ToRead,
            IsDefault = true,
            Books = new List<Book> { books.FirstOrDefault(b => b.Title.Contains("Harry Potter i Kamień Filozoficzny"))! }

        },
        new BookShelf
        {
            Id = Guid.NewGuid(),
            Name = "Aktualnie czytane",
            Description = "Lista książek aktualnie czytanych",
            UserId = _dbContext.Users.FirstOrDefault(u => u.Email == "user@email.com")!.Id,
            Type = BookShelfType.Reading,
            IsDefault = true
        }
    };
    }
}
