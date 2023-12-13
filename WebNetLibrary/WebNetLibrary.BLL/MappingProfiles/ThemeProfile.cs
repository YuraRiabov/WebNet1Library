using AutoMapper;
using WebNetLibrary.Common.Contracts.Theme;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.MappingProfiles;

public class ThemeProfile : Profile
{
    public ThemeProfile()
    {
        CreateMap<Theme, ThemeDto>();
        CreateMap<ThemeDto, Theme>();
        CreateMap<CreateThemeDto, Theme>();
    }
}