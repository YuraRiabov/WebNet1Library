using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebNetLibrary.API.Tests.Integration.Setup;
using WebNetLibrary.Common.Contracts.Author;
using WebNetLibrary.Common.Contracts.Book;
using WebNetLibrary.Common.Contracts.Theme;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;
using Xunit;

namespace WebNetLibrary.API.Tests.Integration.Tests.Abstract;

public class TestBase : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
{
    protected readonly HttpClient Client;
    protected readonly LibraryContext Context;

    protected TestBase(WebApplicationFactory<Startup> factory)
    {
        Client = factory.CreateClient();

        var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
        optionsBuilder.UseInMemoryDatabase("TestDB");
        Context = new LibraryContext(optionsBuilder.Options);
    }

    public virtual void Dispose()
    {
        Context.Database.EnsureDeleted();
    }

    protected async Task SetupBooks()
    {
        var authors = Enumerable.Range(1, 10).Select(number => new CreateAuthorDto { Name = number.ToString() }).ToList();
        var themes = Enumerable.Range(1, 20).Select(number => new CreateThemeDto { Name = number.ToString() }).ToList();
        var books =  Enumerable.Range(1, 95).Select(number => new CreateBookDto
        {
            Name = number.ToString(),
            AuthorIds = new List<long> { number % 9 + 1, number % 9 + 2  },
            ThemeIds = new List<long> { number % 19 + 1, number % 19 + 2 }
        });

        // foreach (var author in authors)
        // {
        //     var response = await Client.PostAsJsonAsync("authors", author);
        // }
        //
        // foreach (var theme in themes)
        // {
        //     var response = await Client.PostAsJsonAsync("themes", theme);
        // }
        //
        // foreach (var book in books)
        // {
        //     var response = await Client.PostAsJsonAsync("books", book);
        //
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         var point = 1;
        //     }
        // }
        

        var authorCreationTasks = authors.Select(author => Client.PostAsJsonAsync("authors", author));
        var themeCreationTasks = themes.Select(theme => Client.PostAsJsonAsync("themes", theme));
        
        await Task.WhenAll(authorCreationTasks);
        await Task.WhenAll(themeCreationTasks);
        
        var bookCreationTasks = books.Select(book => Client.PostAsJsonAsync("books", book));
        await Task.WhenAll(bookCreationTasks);
    }

    protected async Task<T> GetResponse<T>(HttpResponseMessage response)
    {
        var contentString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(contentString)!;
    }
}