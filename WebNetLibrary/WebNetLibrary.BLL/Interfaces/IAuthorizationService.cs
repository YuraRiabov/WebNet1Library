using WebNetLibrary.Common.Contracts.User;
using WebNetLibrary.Common.Contracts.User.Auth0;

namespace WebNetLibrary.BLL.Interfaces;

public interface IAuthorizationService
{
    public Task<bool> CreateUser(CreateUserDto dto);
    public Task<TokenResponse?> GetToken(GetTokenDto dto);
}