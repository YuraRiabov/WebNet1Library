using WebNetLibrary.DAL.Entities.Abstract;

namespace WebNetLibrary.DAL.Entities;

public class User: NamedEntity<long>
{
    public List<UserBook> Books { get; set; } = new();
}