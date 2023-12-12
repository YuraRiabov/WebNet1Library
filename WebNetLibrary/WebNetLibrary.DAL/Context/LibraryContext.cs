using Microsoft.EntityFrameworkCore;
using WebNetLibrary.DAL.Context.Configurations;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.DAL.Context;

public class LibraryContext : DbContext
{
    public virtual DbSet<Author> Authors { get; private set; }
    public virtual DbSet<Book> Books { get; private set; }
    public virtual DbSet<Theme> Themes { get; private set; }
    public virtual DbSet<User> Users { get; private set; }
    public virtual DbSet<UserBook> UserBooks { get; private set; }
    public virtual DbSet<BookAuthor> BookAuthors { get; private set; }
    public virtual DbSet<BookTheme> BookThemes { get; private set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
        Authors = Set<Author>();
        Books = Set<Book>();
        Themes = Set<Theme>();
        Users = Set<User>();
        UserBooks = Set<UserBook>();
        BookAuthors = Set<BookAuthor>();
        BookThemes = Set<BookTheme>();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfig).Assembly);
    }
}