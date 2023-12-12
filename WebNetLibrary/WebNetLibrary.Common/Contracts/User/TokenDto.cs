namespace WebNetLibrary.Common.Contracts.User;

public class TokenDto
{
    public required long UserId { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}