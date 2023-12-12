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

    public PortfolioService(LibraryContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<bool> Add(long userId, long bookId)
    {
        var currentPortfolioSize = await GetCurrentPortfolioSize(userId);
        var bookToAdd = await Context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == bookId);
        var user = await Context.Users.Include(user => user.Books).FirstOrDefaultAsync(u => u.Id == userId);
        if (currentPortfolioSize >= _maxPortfolioSize || bookToAdd == null || user == null)
        {
            return false;
        }

        user.Books.Add(new UserBook { Book = bookToAdd });
        
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

    public async Task<List<long>> GetBookIds(long userId)
    {
        return await Context.UserBooks.Where(ub => ub.UserId == userId).Select(ub => ub.BookId).ToListAsync();
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