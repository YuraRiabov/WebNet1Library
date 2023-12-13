using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.DAL.Context.Configurations;

public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.HasKey(b => new { b.BookId, b.AuthorId });
    }
}