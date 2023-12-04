namespace WebNetLibrary.DAL.Entities.Abstract;

public abstract class Entity<T> where T: struct
{
    public T Id { get; set; }
}