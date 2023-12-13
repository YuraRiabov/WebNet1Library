namespace WebNetLibrary.Common.Exceptions;

public class InvalidCredentialsException : BaseException
{
    public override int StatusCode { get; protected set; } = 400;

    public InvalidCredentialsException() : base($"Invalid credentials")
    {
    }
}