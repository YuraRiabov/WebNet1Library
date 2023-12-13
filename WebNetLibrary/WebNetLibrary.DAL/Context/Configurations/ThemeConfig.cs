using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.DAL.Context.Configurations;

public class ThemeConfig : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> builder)
    {
        builder.Property(s => s.Id)
            .IsRequired();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(b => b.Books)
            .WithOne(b => b.Theme)
            .HasForeignKey(b => b.ThemeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}