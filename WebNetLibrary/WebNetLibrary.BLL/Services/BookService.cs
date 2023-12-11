using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Abstract;
using WebNetLibrary.Common.Contracts.Book;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.Services;

public class BookService : BaseService, IBookService
{
    private readonly IAuthorService _authorService;
    private readonly IThemeService _themeService;
    public BookService(LibraryContext context, IMapper mapper, IAuthorService authorService, IThemeService themeService) : base(context, mapper)
    {
        _authorService = authorService;
        _themeService = themeService;
    }

    public async Task<long> Create(CreateBookDto dto)
    {
        if (!ValidateBookAuthors(dto.AuthorIds) || !ValidateBookThemes(dto.ThemeIds))
        {
            throw new ArgumentException();
        }
        
        var book = Mapper.Map<Book>(dto);
        var createdBook = Context.Books.Add(book).Entity;
        await Context.SaveChangesAsync();
        
        Context.BookAuthors.AddRange(CreateBookAuthors(createdBook.Id, dto.AuthorIds));
        Context.BookThemes.AddRange(CreateBookThemes(createdBook.Id, dto.ThemeIds));
        await Context.SaveChangesAsync();

        return createdBook.Id;
    }

    public async Task Update(UpdateBookDto dto)
    {
        if (!ValidateBookAuthors(dto.AuthorIds) || !ValidateBookThemes(dto.ThemeIds))
        {
            throw new ArgumentException();
        }
        
        var book = await GetInternal(dto.Id);
        book = Mapper.Map(dto, book);
        Context.Books.Update(book);

        UpdateBookAuthors(dto);
        UpdateBookThemes(dto);

        await Context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var author = await GetInternal(id);
        Context.Books.Remove(author);
        await Context.SaveChangesAsync();
    }

    public async Task<BookDto?> Get(long id)
    {
        var book = await Context.Books
            .Include(b => b.Authors)
            .ThenInclude(a => a.Author)
            .Include(b => b.Themes)
            .ThenInclude(t => t.Theme)
            .FirstOrDefaultAsync(b => b.Id == id);

        return Mapper.Map<BookDto>(book);
    }

    public async Task<List<BookDto>> Search(string? nameFilter, List<long>? authorIds, List<long>? themeIds)
    {
        var books = await Context.Books
            .Include(b => b.Authors)
            .Include(b => b.Themes)
            .Where(b =>
                (nameFilter == null || b.Name.Contains(nameFilter)) &&
                (authorIds == null || authorIds.Count == 0 || b.Authors.Any(a => authorIds.Contains(a.AuthorId))) &&
                (themeIds == null || themeIds.Count == 0 || b.Themes.Any(t => themeIds.Contains(t.ThemeId)))
            ).ToListAsync();
        return Mapper.Map<List<BookDto>>(books);
    }

    private async Task<Book> GetInternal(long id)
    {
        return await Context.Books.FirstOrDefaultAsync(a => a.Id == id) ?? throw new ArgumentException(nameof(id));
    }

    private IEnumerable<BookAuthor> CreateBookAuthors(long bookId, List<long> authorIds)
    {
        return authorIds.Select(id => new BookAuthor
        {
            AuthorId = id,
            BookId = bookId
        });
    }
    
    private IEnumerable<BookTheme> CreateBookThemes(long bookId, List<long> themeIds)
    {
        return themeIds.Select(id => new BookTheme
        {
            ThemeId = id,
            BookId = bookId
        });
    }
    private void UpdateBookThemes(UpdateBookDto dto)
    {
        var currentThemes = Context.BookThemes.Where(bt => bt.BookId == dto.Id);
        if (!currentThemes.Select(bt => bt.ThemeId).SequenceEqual(dto.ThemeIds))
        {
            Context.BookThemes.RemoveRange(currentThemes);
            Context.BookThemes.AddRange(CreateBookThemes(dto.Id, dto.ThemeIds));
        }
    }

    private void UpdateBookAuthors(UpdateBookDto dto)
    {
        var currentAuthors = Context.BookAuthors.Where(ba => ba.BookId == dto.Id);
        if (!currentAuthors.Select(ba => ba.AuthorId).SequenceEqual(dto.AuthorIds))
        {
            Context.BookAuthors.RemoveRange(currentAuthors);
            Context.BookAuthors.AddRange(CreateBookAuthors(dto.Id, dto.AuthorIds));
        }
    }

    private bool ValidateBookAuthors(List<long> authorIds)
    {
        var allAuthorIds = _authorService.Get().Select(a => a.Id);
        return authorIds.All(id => allAuthorIds.Contains(id));
    }
    
    private bool ValidateBookThemes(List<long> authorIds)
    {
        var allThemeIds = _themeService.Get().Select(t => t.Id);
        return authorIds.All(id => allThemeIds.Contains(id));
    }
}