namespace WebNetLibrary.DAL.Entities;

public class BookTheme
{
    public long BookId { get; set; }
    public long ThemeId { get; set; }

    public Book Book { get; set; } = new();
    public Theme Theme { get; set; } = new();
}