using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
        }
    }
}