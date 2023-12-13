using WebNetLibrary.DAL.Entities.Abstract;

namespace WebNetLibrary.DAL.Entities;

public class Author: NamedEntity<long>
{
    public List<BookAuthor> Books { get; set; } = new();
}