using WebNetLibrary.DAL.Entities.Abstract;

namespace WebNetLibrary.DAL.Entities;

public class Book: NamedEntity<long>
{
    public List<BookAuthor> Authors { get; set; } = new();
    public List<BookTheme> Themes { get; set; } = new();
    public List<UserBook> Users { get; set; } = new();
}