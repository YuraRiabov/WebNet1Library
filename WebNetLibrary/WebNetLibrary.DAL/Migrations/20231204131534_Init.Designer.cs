﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebNetLibrary.DAL.Context;

#nullable disable

namespace WebNetLibrary.DAL.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20231204131534_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.BookAuthor", b =>
                {
                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.BookTheme", b =>
                {
                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<long>("ThemeId")
                        .HasColumnType("bigint");

                    b.HasKey("BookId", "ThemeId");

                    b.HasIndex("ThemeId");

                    b.ToTable("BookThemes");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.Theme", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.UserBook", b =>
                {
                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("BookId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBooks");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.BookAuthor", b =>
                {
                    b.HasOne("WebNetLibrary.DAL.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebNetLibrary.DAL.Entities.Book", "Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.BookTheme", b =>
                {
                    b.HasOne("WebNetLibrary.DAL.Entities.Book", "Book")
                        .WithMany("Themes")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebNetLibrary.DAL.Entities.Theme", "Theme")
                        .WithMany("Books")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.UserBook", b =>
                {
                    b.HasOne("WebNetLibrary.DAL.Entities.Book", "Book")
                        .WithMany("Users")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebNetLibrary.DAL.Entities.User", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Themes");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.Theme", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("WebNetLibrary.DAL.Entities.User", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
