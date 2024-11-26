using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaczytani.Domain.Entities;
public class UserBook
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime DrawnDate { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }
}
