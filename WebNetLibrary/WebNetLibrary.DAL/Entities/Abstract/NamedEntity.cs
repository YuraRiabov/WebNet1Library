namespace WebNetLibrary.DAL.Entities.Abstract;

public abstract class NamedEntity<T>: Entity<T> where T: struct
{
    public string Name { get; set; } = string.Empty;
}