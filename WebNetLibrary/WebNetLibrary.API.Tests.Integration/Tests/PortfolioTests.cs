using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using WebNetLibrary.API.Tests.Integration.Setup;
using WebNetLibrary.API.Tests.Integration.Tests.Abstract;
using WebNetLibrary.Common.Contracts.Book;
using WebNetLibrary.Common.Contracts.User;
using Xunit;

namespace WebNetLibrary.API.Tests.Integration.Tests;

[Collection("DbUsingTests")]
public class PortfolioTests : TestBase
{
    private const string UsersUrl = "users";
    
    public PortfolioTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddBookToPortfolio_WhenEmpty_ShouldAdd()
    {
        await SetupBooks();

        var userId = await SetupUser();
        var bookId = (await GetAllBooks()).First().Id;

        var response = await Client.PostAsync(GetPortfolioMutationUrl(userId, bookId), null);

        var userBooks = await GetUserBooks(userId);
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Single(userBooks);
        Assert.Equal(userBooks[0], bookId);
    }

    [Fact]
    public async Task RemoveBook_ShouldWork()
    {
        await SetupBooks();

        var userId = await SetupUser();
        var bookId = (await GetAllBooks()).First().Id;

        await Client.PostAsync(GetPortfolioMutationUrl(userId, bookId), null);
        var response = await Client.DeleteAsync(GetPortfolioMutationUrl(userId, bookId));

        var userBooks = await GetUserBooks(userId);
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Empty(userBooks);
    }

    [Fact]
    public async Task AddBook_WhenReachedLimit_ShouldNotAdd()
    {
        
        await SetupBooks();

        var userId = await SetupUser();
        var books = await GetAllBooks();

        for (int i = 0; i < 10; i++)
        {
            await Client.PostAsync(GetPortfolioMutationUrl(userId, books[i].Id), null);
        }

        var response = await Client.PostAsync(GetPortfolioMutationUrl(userId, books[11].Id), null);

        var userBooks = await GetUserBooks(userId);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(10, userBooks.Count);
    }

    private async Task<long> SetupUser()
    {
        return await GetResponse<long>(await Client.PostAsJsonAsync(UsersUrl, new CreateUserDto
        {
            Email = "test@gmail.com",
            Password = "P@ssw0rd",
            Username = "test"
        }));
    }

    private string GetPortfolioMutationUrl(long userId, long bookId)
    {
        return $"{GetPortfolioUrl(userId)}/{bookId}";
    }

    private string GetPortfolioUrl(long userId)
    {
        return $"{UsersUrl}/{userId}/portfolio";
    }

    private async Task<List<long>> GetUserBooks(long userId)
    {
        return await GetResponse<List<long>>(await Client.GetAsync(GetPortfolioUrl(userId)));
    }

    private async Task<List<BookDto>> GetAllBooks()
    {
        return await GetResponse<List<BookDto>>(await Client.GetAsync("books"));
    }
}