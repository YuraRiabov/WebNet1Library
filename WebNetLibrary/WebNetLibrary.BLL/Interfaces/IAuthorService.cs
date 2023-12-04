using WebNetLibrary.Common.Contracts.Author;

namespace WebNetLibrary.BLL.Interfaces;

public interface IAuthorService : IEntityMutationService<AuthorDto, CreateAuthorDto, AuthorDto>
{
    public Task<List<AuthorDto>> Get();
}