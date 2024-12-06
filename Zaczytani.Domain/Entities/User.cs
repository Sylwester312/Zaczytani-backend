
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Zaczytani.Domain.Entities;
public class User : IdentityUser<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid Id
    {
        get { return base.Id; }
        set { base.Id = value; }
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<UserDrawnBook> UserBooks { get; set; } = new List<UserDrawnBook>();
    public ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();
}

public class UserRole : IdentityRole<Guid>
{
    public UserRole() : base() { }

    public UserRole(string roleName) : base(roleName)
    {
        Name = roleName;
    }
}