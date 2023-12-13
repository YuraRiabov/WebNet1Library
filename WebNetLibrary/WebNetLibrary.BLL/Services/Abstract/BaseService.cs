using AutoMapper;
using WebNetLibrary.DAL.Context;

namespace WebNetLibrary.BLL.Services.Abstract;

public abstract class BaseService
{
    protected readonly LibraryContext Context;
    protected readonly IMapper Mapper;

    public BaseService(LibraryContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
}