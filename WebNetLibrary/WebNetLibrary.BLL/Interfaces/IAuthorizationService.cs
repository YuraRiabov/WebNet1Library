using WebNetLibrary.Common.Contracts.User;

namespace WebNetLibrary.BLL.Interfaces;

public interface IAuthorizationService
{
    public Task<bool> CreateUser(CreateUserDto dto);
}