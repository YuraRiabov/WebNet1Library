namespace WebNetLibrary.Common.Exceptions;

public class EntityNotFoundException<T> : BaseException
{
    public override int StatusCode { get; protected set; } = 404;

    public EntityNotFoundException() : base($"{nameof(T)} entity not found")
    {
    }
}