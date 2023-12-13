using WebNetLibrary.Common.Contracts.Author;

namespace WebNetLibrary.BLL.Interfaces;

public interface IAuthorService : IEntityService<AuthorDto, CreateAuthorDto, AuthorDto>
{
    public List<AuthorDto> Get();
}