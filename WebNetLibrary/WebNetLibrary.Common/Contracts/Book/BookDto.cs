using WebNetLibrary.Common.Contracts.Abstract;
using WebNetLibrary.Common.Contracts.Author;
using WebNetLibrary.Common.Contracts.Theme;

namespace WebNetLibrary.Common.Contracts.Book;

public class BookDto : NamedEntityDto<long>
{
    public List<AuthorDto> Authors { get; set; } = new();
    public List<ThemeDto> Themes { get; set; } = new();
}