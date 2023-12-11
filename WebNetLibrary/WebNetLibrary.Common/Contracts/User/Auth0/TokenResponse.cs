using System.Text.Json.Serialization;

namespace WebNetLibrary.Common.Contracts.User.Auth0;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}