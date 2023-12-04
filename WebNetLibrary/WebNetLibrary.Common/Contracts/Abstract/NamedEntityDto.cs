namespace WebNetLibrary.Common.Contracts.Abstract;

public abstract class NamedEntityDto<T> where T: struct
{
    public string Name { get; set; } = string.Empty;
    public T Id { get; set; }
}