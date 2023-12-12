using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.MappingProfiles;
using WebNetLibrary.BLL.Services;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;
using Xunit;

namespace WebNetLibrary.BLL.Tests.Unit.Tests;

public class BookServiceTests
{
    private BookService _bookService;
    private readonly IMapper _mapper;
    private readonly LibraryContext _context;

    public BookServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BookProfile>();
            cfg.AddProfile<AuthorProfile>();
            cfg.AddProfile<ThemeProfile>();
        });
        _mapper = config.CreateMapper();
        _context = FakeContext();
        A.CallTo(() => _context.Books).Returns(FakeDbSet(GetTestBooks()));

        _bookService = new BookService(_context, _mapper, A.Fake<IAuthorService>(), A.Fake<IThemeService>());
    }

    [Theory]
    [MemberData(nameof(GetTestDataForQueueFill))]
    public void SearchBooks_WhenNoFilter_ShouldReturnAll(string? nameFilter, List<long>? authorIds, List<long>? themeIds, int expectedCount)
    {
        var result = _bookService.Search(nameFilter, authorIds, themeIds);
        
        Assert.Equal(expectedCount, result.Count);
    }
    
    public static IEnumerable<object[]> GetTestDataForQueueFill()
    {
        yield return new object[] { null, null, null, 95 };
        yield return new object[] { "9", null, null, 15 };
        yield return new object[] { "9", new List<long>() { 9 }, null, 5 };
        yield return new object[] { "9", new List<long>() { 9 }, new List<long>() { 9 }, 0 };
    }
    
    private LibraryContext FakeContext()
    {
        return A.Fake<LibraryContext>(c =>
            c.WithArgumentsForConstructor(() => new LibraryContext(new DbContextOptions<LibraryContext>())));
    }

    private DbSet<T> FakeDbSet<T>(IQueryable<T> dataSource) where T : class
    {
        var fakeDbSet = A.Fake<DbSet<T>>(d => 
            d.Implements(typeof(IQueryable<T>)));
        
        A.CallTo(() => ((IQueryable<T>)fakeDbSet).GetEnumerator())
            .Returns(dataSource .GetEnumerator());
        A.CallTo(() => ((IQueryable<T>)fakeDbSet).Provider)
            .Returns(dataSource .Provider);
        A.CallTo(() => ((IQueryable<T>)fakeDbSet).Expression)
            .Returns(dataSource .Expression);
        A.CallTo(() => ((IQueryable<T>)fakeDbSet).ElementType)
            .Returns(dataSource .ElementType);

        return fakeDbSet;
    }

    private IQueryable<Book> GetTestBooks()
    {
        return Enumerable.Range(1, 95).Select(number => new Book()
        {
            Id = number,
            Name = number.ToString(),
            Themes = new List<BookTheme>()
            {
                new BookTheme()
                {
                    BookId = number,
                    ThemeId = number % 10 + 1
                }
            },
            Authors = new List<BookAuthor>()
            {
                new BookAuthor()
                {
                    BookId = number,
                    AuthorId = number % 20
                }
            }
        }).AsQueryable();
    }
}