using WebNetLibrary.Common.Contracts.Book;

namespace WebNetLibrary.BLL.Interfaces;

public interface IBookService : IEntityMutationService<BookDto, CreateBookDto, UpdateBookDto>
{
    public Task<List<BookDto>> Search(string nameFilter, List<long> authorIds, List<long> themeIds);
}