using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaczytani.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        // Kolekcja książek, aby odzwierciedlić relację wiele-do-wielu z książkami
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
