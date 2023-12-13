using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.DAL.Context.Configurations;

public class BookThemeConfig : IEntityTypeConfiguration<BookTheme>
{
    public void Configure(EntityTypeBuilder<BookTheme> builder)
    {
        builder.HasKey(b => new { b.BookId, b.ThemeId });
    }
}