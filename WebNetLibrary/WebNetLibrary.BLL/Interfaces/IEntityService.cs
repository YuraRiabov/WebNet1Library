namespace WebNetLibrary.BLL.Interfaces;

public interface IEntityService<TDto, TCreateDto, TUpdateDto>
{
    public Task<long> Create(TCreateDto dto);
    public Task Update(TUpdateDto dto);
    public Task Delete(long id);
    public Task<TDto?> Get(long id);
}