namespace WebNetLibrary.DAL.Entities;

public class UserBook
{
    public long UserId { get; set; }
    public long BookId { get; set; }

    public Book Book { get; set; } = new();
    public User User { get; set; } = new();
}