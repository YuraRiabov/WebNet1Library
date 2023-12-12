using System.Text.Json.Serialization;

namespace WebNetLibrary.Common.Contracts.User.Auth0;

public class GetUserTokenAuth0Request
{
    [JsonPropertyName("grant_type")] 
    public string GrantType { get; set; } = "password";

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

    [JsonPropertyName("scope")] 
    public string? Scope { get; set; } = "read:catalogue";

    [JsonPropertyName("connection")]
    public string Connection => "Username-Password-Authentication";
}