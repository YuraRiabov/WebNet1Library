using WebNetLibrary.Common.Contracts.Theme;

namespace WebNetLibrary.BLL.Interfaces;

public interface IThemeService : IEntityService<ThemeDto, CreateThemeDto, ThemeDto>
{
    public List<ThemeDto> Get();
}