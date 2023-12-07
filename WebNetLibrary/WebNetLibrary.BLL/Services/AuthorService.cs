using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Abstract;
using WebNetLibrary.Common.Contracts.Author;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.Services;

public class AuthorService : BaseService, IAuthorService
{
    public AuthorService(LibraryContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    public async Task<long> Create(CreateAuthorDto dto)
    {
        var author = Mapper.Map<Author>(dto);
        var createdAuthor = Context.Authors.Add(author).Entity;
        await Context.SaveChangesAsync();

        return createdAuthor.Id;
    }

    public async Task Update(AuthorDto dto)
    {
        var author = await GetInternal(dto.Id);
        author = Mapper.Map(dto, author);
        Context.Authors.Update(author);

        await Context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var author = await GetInternal(id);
        Context.Authors.Remove(author);
        await Context.SaveChangesAsync();
    }

    public List<AuthorDto> Get()
    {
        return Mapper.Map<List<AuthorDto>>(Context.Authors);
    }

    public async Task<AuthorDto?> Get(long id)
    {
        return Mapper.Map<AuthorDto>(await GetInternal(id));
    }

    private async Task<Author> GetInternal(long id)
    {
        return await Context.Authors.FirstOrDefaultAsync(a => a.Id == id) ?? throw new ArgumentException(nameof(id));
    }
}