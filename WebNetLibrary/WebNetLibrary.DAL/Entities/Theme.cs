using WebNetLibrary.DAL.Entities.Abstract;

namespace WebNetLibrary.DAL.Entities;

public class Theme: NamedEntity<long>
{
    public List<BookAuthor> Books { get; set; } = new();
}