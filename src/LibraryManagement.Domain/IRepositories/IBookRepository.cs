using LibraryManagement.Domain.Models;

namespace LibraryManagement.Domain.IRepositories
{
    public interface IBookRepository : IGenericRepository<Book, int>
    {
    }
}
