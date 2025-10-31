using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.Year)
                .IsRequired(false);

            builder.Property(b => b.IsAvailable)
                .HasDefaultValue(true);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}