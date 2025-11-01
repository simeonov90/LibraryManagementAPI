using LibraryManagement.Domain.IRepositories;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book, int>, IBookRepository
    {
        public BookRepository(LibraryDbContext libraryDbContext) 
            : base(libraryDbContext)
        {
        }
    }
}
