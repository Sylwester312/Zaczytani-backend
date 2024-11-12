using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Constants;
using Zaczytani.Domain.Entities;
using Zaczytani.Infrastructure.Persistance;

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
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter i Komnata Tajemnic",
                Description = "Drugi tom bestsellerowego cyklu w poważnej, \"dorosłej\" okładce.\r\nTym razem Harry będzie musiał się zmierzyć z tajemniczym potworem z komnaty tajemnic na zamku Hogwart. Otworzyć tę komnatę mógł jedynie prawowity dziedzic Slytherina, a wskutek nieszczęśliwego zbiegu okoliczności podejrzenie pada na Harryego. W dodatku jeden z najbliższych przyjaciół bohatera znajduje się w śmiertelnym niebezpieczeństwie. Z tego tomu dowiecie się, jaka była przeszłość Hagrida, jakie sekrety skrywa rodzina Malfoya i kto naprawdę wypuścił potwora.",
                Authors = [_dbContext.Authors.FirstOrDefault(a => a.Name.Contains("J.K"))],
            }
            ];

        return books;

    }

}
