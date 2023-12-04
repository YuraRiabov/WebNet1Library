using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.DAL.Context.Configurations;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(s => s.Id)
            .IsRequired();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(b => b.Authors)
            .WithOne(a => a.Book)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(b => b.Users)
            .WithOne(u => u.Book)
            .HasForeignKey(u => u.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(b => b.Themes)
            .WithOne(t => t.Book)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}