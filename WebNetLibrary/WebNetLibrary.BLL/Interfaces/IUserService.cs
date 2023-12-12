using WebNetLibrary.Common.Contracts.User;

namespace WebNetLibrary.BLL.Interfaces;

public interface IUserService
{
    public Task<long> CreateUser(CreateUserDto dto);
    public Task<TokenDto> GetToken(GetTokenDto dto);
}