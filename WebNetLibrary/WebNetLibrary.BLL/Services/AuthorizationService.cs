using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Options;
using WebNetLibrary.Common.Contracts.User;
using WebNetLibrary.Common.Contracts.User.Auth0;

namespace WebNetLibrary.BLL.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly Auth0Options _options;
    private readonly HttpClient _httpClient;

    private const string CreateUserEndpoint = "/api/v2/users";
    private const string GetTokenEndpoint = "/oauth/token";
    
    private static string? ApplicationToken { get; set; }

    public AuthorizationService(IOptions<Auth0Options> options, HttpClient httpClient)
    {
        _options = options.Value;
        _httpClient = httpClient;
        
        SetAuthorizationHeader();
    }
    public async Task<bool> CreateUser(CreateUserDto dto)
    {
        var request = new CreateUserAuth0Request
        {
            Email = dto.Email,
            Password = dto.Password,
            Username = dto.Username
        };

        var response = await CreateUserWithoutRetry(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await RefreshApplicationToken();
            response = await CreateUserWithoutRetry(request);
        }
        
        return response.IsSuccessStatusCode;
    }

    public async Task<TokenResponse?> GetToken(GetTokenDto dto)
    {
        var request = new GetUserTokenAuth0Request
        {
            Audience = _options.UserManagementAudience,
            ClientId = _options.ClientId,
            ClientSecret = _options.ClientSecret,
            Password = dto.Password,
            Username = dto.Username
        };

        var uri = _options.Domain + GetTokenEndpoint;
        var tokenResponse = await _httpClient.PostAsJsonAsync(uri, request);
        if (!tokenResponse.IsSuccessStatusCode)
        {
            return null;
        }
        return await tokenResponse.Content.ReadFromJsonAsync<TokenResponse>();
    }

    private async Task RefreshApplicationToken()
    {
        var uri = _options.Domain + GetTokenEndpoint;

        var tokenResponse = await _httpClient.PostAsJsonAsync(uri, new GetApplicationTokenRequest
        {
            Audience = _options.UserManagementAudience,
            ClientId = _options.ClientId,
            ClientSecret = _options.ClientSecret,
            GrantType = "client_credentials"
        });

        var token = await tokenResponse.Content.ReadFromJsonAsync<TokenResponse>();

        ApplicationToken = token!.AccessToken;
        SetAuthorizationHeader();
    }

    private async Task<HttpResponseMessage> CreateUserWithoutRetry(CreateUserAuth0Request request)
    {
        var uri = _options.Domain + CreateUserEndpoint;
        return await _httpClient.PostAsJsonAsync(uri, request);
    }

    private void SetAuthorizationHeader()
    {
        if (ApplicationToken != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApplicationToken);
        }
    }
}