
namespace Zaczytani.Application.Dtos;

public class BookShelfDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsDefault { get; set; }
}