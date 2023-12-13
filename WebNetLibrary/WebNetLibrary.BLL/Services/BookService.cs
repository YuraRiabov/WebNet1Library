using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Abstract;
using WebNetLibrary.Common.Contracts.Book;
using WebNetLibrary.Common.Exceptions;
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
        if (!ValidateBookAuthors(dto.AuthorIds))
        {
            throw new EntityNotFoundException<Author>();
        }
        
        if (!ValidateBookThemes(dto.AuthorIds))
        {
            throw new EntityNotFoundException<Theme>();
        }
        
        var book = Mapper.Map<Book>(dto);
        var createdBook = Context.Books.Add(book).Entity;
            
        book.Authors = GetBookAuthors(dto.AuthorIds);
        book.Themes = GetBookThemes(dto.ThemeIds);
        
        await Context.SaveChangesAsync();

        return createdBook.Id;
    }

    public async Task Update(UpdateBookDto dto)
    {
        if (!ValidateBookAuthors(dto.AuthorIds))
        {
            throw new EntityNotFoundException<Author>();
        }
        
        if (!ValidateBookThemes(dto.AuthorIds))
        {
            throw new EntityNotFoundException<Theme>();
        }
        
        var book = await GetInternal(dto.Id);
        book = Mapper.Map(dto, book);
        Context.Books.Update(book);

        book.Authors = GetBookAuthors(dto.AuthorIds);
        book.Themes = GetBookThemes(dto.ThemeIds);

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

    public List<BookDto> Search(string? nameFilter, List<long>? authorIds, List<long>? themeIds)
    {
        var books = Context.Books
            .Include(b => b.Authors)
            .Include(b => b.Themes)
            .Where(b =>
                (nameFilter == null || b.Name.Contains(nameFilter)) &&
                (authorIds == null || authorIds.Count == 0 || b.Authors.Any(a => authorIds.Contains(a.AuthorId))) &&
                (themeIds == null || themeIds.Count == 0 || b.Themes.Any(t => themeIds.Contains(t.ThemeId)))
            ).ToList();
        return Mapper.Map<List<BookDto>>(books);
    }

    private async Task<Book> GetInternal(long id)
    {
        return await Context.Books.FirstOrDefaultAsync(a => a.Id == id) ?? throw new EntityNotFoundException<Book>();
    }

    private List<BookAuthor> GetBookAuthors(List<long> authorIds)
    {
        return Context.Authors
            .Where(a => authorIds.Contains(a.Id))
            .Select(author => new BookAuthor { Author = author })
            .ToList();
    }
    
    private List<BookTheme> GetBookThemes(List<long> themeIds)
    {
        return Context.Themes
            .Where(t => themeIds.Contains(t.Id))
            .Select(theme => new BookTheme { Theme = theme })
            .ToList();
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