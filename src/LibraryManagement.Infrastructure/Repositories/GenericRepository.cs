using LibraryManagement.Domain.IRepositories;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly LibraryDbContext _libraryDbContext;

        public GenericRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _libraryDbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _libraryDbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _libraryDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _libraryDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _libraryDbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _libraryDbContext.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _libraryDbContext.SaveChangesAsync();
        }
    }
}
