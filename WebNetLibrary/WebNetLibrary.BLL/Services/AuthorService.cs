using AutoMapper;
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
    
    public async Task<AuthorDto> Create(CreateAuthorDto dto)
    {
        var author = Mapper.Map<Author>(dto);
        var createdAuthor = Context.Authors.Add(author).Entity;
        await Context.SaveChangesAsync();

        return Mapper.Map<AuthorDto>(createdAuthor);
    }

    public Task<AuthorDto> Update(AuthorDto dto)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<AuthorDto>> Get()
    {
        throw new NotImplementedException();
    }
}