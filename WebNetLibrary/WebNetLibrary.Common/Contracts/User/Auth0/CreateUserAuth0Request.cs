using System.Text.Json.Serialization;

namespace WebNetLibrary.Common.Contracts.User.Auth0;

public class CreateUserAuth0Request
{
    
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("user_metadata")] 
    public UserMetaData UserMetadata { get; init; } = new();

    [JsonPropertyName("password")]
    public string? Password { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("connection")]
    public string Connection => "Username-Password-Authentication";

    [JsonPropertyName("blocked")] 
    public bool Blocked => false;

    [JsonPropertyName("email_verified")] 
    public bool EmailVerified => true;
}