using AutoMapper;
using WebNetLibrary.Common.Contracts.Book;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.MappingProfiles;

public class BookProfile: Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<UpdateBookDto, BookDto>();
        CreateMap<CreateBookDto, BookDto>();
    }
}