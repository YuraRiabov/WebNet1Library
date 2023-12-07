using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Abstract;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.Services;

public class PortfolioService : BaseService, IPortfolioService
{
    private readonly int _maxPortfolioSize = 10;
    private readonly IBookService _bookService;

    public PortfolioService(LibraryContext context, IMapper mapper, IBookService bookService) : base(context, mapper)
    {
        _bookService = bookService;
    }

    public async Task<bool> Add(long userId, long bookId)
    {
        var currentPortfolioSize = await GetCurrentPortfolioSize(userId);
        var bookToAdd = await _bookService.Get(bookId);
        if (currentPortfolioSize > _maxPortfolioSize || bookToAdd == null)
        {
            return false;
        }

        Context.UserBooks.Add(new UserBook
        {
            UserId = userId,
            BookId = bookId
        });
        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Remove(long userId, long bookId)
    {
        var userBook = await GetUserBook(userId, bookId);
        if (userBook == null)
        {
            return false;
        }

        Context.UserBooks.Remove(userBook);
        await Context.SaveChangesAsync();

        return true;
    }

    private async Task<UserBook?> GetUserBook(long userId, long bookId)
    {
        return await Context.UserBooks.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
    }

    private async Task<int> GetCurrentPortfolioSize(long userId)
    {
        return await Context.UserBooks.Where(ub => ub.UserId == userId).CountAsync();
    }
}