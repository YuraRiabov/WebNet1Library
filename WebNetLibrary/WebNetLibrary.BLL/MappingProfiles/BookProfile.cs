using AutoMapper;
using WebNetLibrary.Common.Contracts.Book;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.MappingProfiles;

public class BookProfile: Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(b => b.Authors, opt 
                => opt.MapFrom(book => book.Authors.Select(a => a.Author)))
            .ForMember(b => b.Themes, opt 
                => opt.MapFrom(book => book.Themes.Select(a => a.Theme)));
        
        CreateMap<UpdateBookDto, BookDto>();
        CreateMap<CreateBookDto, BookDto>();
    }
}