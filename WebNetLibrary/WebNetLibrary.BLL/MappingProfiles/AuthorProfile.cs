using AutoMapper;
using WebNetLibrary.Common.Contracts.Author;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.MappingProfiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();
        CreateMap<CreateAuthorDto, Author>();
    }
}