using LibraryManagement.Domain.IRepositories;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext libraryDbContext) 
            : base(libraryDbContext)
        {
        }
    }
}
