using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebNetLibrary.DAL.Context;

public class LibraryContextFactory: IDesignTimeDbContextFactory<LibraryContext>
{
    public LibraryContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=LibraryDB;Trusted_Connection=True;");

        return new LibraryContext(optionsBuilder.Options);
    }
}