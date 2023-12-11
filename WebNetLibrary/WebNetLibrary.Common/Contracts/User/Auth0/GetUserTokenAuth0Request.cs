using System.Text.Json.Serialization;

namespace WebNetLibrary.Common.Contracts.User.Auth0;

public class GetUserTokenAuth0Request
{
    [JsonPropertyName("grant_type")]
    public required string GrantType { get; set; }

    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }
    
    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }

    [JsonPropertyName("audience")]
    public string? Audience { get; set; }

    public GetUserTokenAuth0Request()
    {
        
    }
}