using WebNetLibrary.Common.Contracts.Theme;

namespace WebNetLibrary.BLL.Interfaces;

public interface IThemeService : IEntityMutationService<ThemeDto, CreateThemeDto, ThemeDto>
{
    public Task<List<ThemeDto>> Get();
}