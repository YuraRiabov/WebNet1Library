using WebNetLibrary.Common.Contracts.Abstract;

namespace WebNetLibrary.Common.Contracts.Book;

public class CreateBookDto : CreateNamedEntityDto
{
    public List<long> AuthorIds { get; set; } = new();
    public List<long> ThemeIds { get; set; } = new();
}