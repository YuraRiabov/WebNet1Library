namespace WebNetLibrary.DAL.Entities;

public class BookAuthor
{
    public long BookId { get; set; }
    public long AuthorId { get; set; }

    public Book Book { get; set; } = new();
    public Author Author { get; set; } = new();
}