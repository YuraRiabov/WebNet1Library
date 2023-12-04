namespace WebNetLibrary.BLL.Interfaces;

public interface IEntityMutationService<TDto, TCreateDto, TUpdateDto>
{
    public Task<TDto> Create(TCreateDto dto);
    public Task<TDto> Update(TUpdateDto dto);
    public Task Delete(long id);
}