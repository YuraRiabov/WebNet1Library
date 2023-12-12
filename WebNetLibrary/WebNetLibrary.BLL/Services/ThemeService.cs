using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Abstract;
using WebNetLibrary.Common.Contracts.Theme;
using WebNetLibrary.Common.Exceptions;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.Services;

public class ThemeService: BaseService, IThemeService
{
    public ThemeService(LibraryContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    public async Task<long> Create(CreateThemeDto dto)
    {
        var theme = Mapper.Map<Theme>(dto);
        var createdTheme = Context.Themes.Add(theme).Entity;
        await Context.SaveChangesAsync();

        return createdTheme.Id;
    }

    public async Task Update(ThemeDto dto)
    {
        var theme = await GetInternal(dto.Id);
        theme = Mapper.Map(dto, theme);
        Context.Themes.Update(theme);

        await Context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var theme = await GetInternal(id);
        Context.Themes.Remove(theme);
        await Context.SaveChangesAsync();
    }

    public List<ThemeDto> Get()
    {
        return Mapper.Map<List<ThemeDto>>(Context.Themes);
    }

    public async Task<ThemeDto?> Get(long id)
    {
        return Mapper.Map<ThemeDto>(await GetInternal(id));
    }

    private async Task<Theme> GetInternal(long id)
    {
        return await Context.Themes.FirstOrDefaultAsync(a => a.Id == id) ?? throw new EntityNotFoundException<Theme>();
    }
}