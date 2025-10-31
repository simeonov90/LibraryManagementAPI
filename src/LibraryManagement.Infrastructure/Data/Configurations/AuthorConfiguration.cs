using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                .IsRequired();

            builder.Property(a => a.LastName)
                .IsRequired();

            builder.Property(a => a.DateOfBirth)
                .IsRequired(false);

            builder.Property(a => a.Country)
                .IsRequired(false);
        }
    }
}