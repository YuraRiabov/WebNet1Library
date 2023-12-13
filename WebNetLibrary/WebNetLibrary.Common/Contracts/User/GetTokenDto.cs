namespace WebNetLibrary.Common.Contracts.User;

public class GetTokenDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}