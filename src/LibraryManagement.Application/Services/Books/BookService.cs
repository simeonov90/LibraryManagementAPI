using AutoMapper;
using LibraryManagement.Application.Services.Authors.Dtos;
using LibraryManagement.Application.Services.Books.Dtos;
using LibraryManagement.Application.Services.Shared.Dtos;
using LibraryManagement.Domain.IRepositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Services.Books
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<BookDto>> GetPagedResultAsync(BookFilterDto filter)
        {
            var query = _bookRepository
                .GetAll()
                .Include(b => b.Author)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(term) ||
                    b.Author.FirstName.ToLower().Contains(term) ||
                    b.Author.LastName.ToLower().Contains(term));
            }

            query = filter.SortOrder.ToLower() == "asc"
                ? query.OrderBy(b => b.Id)
                : query.OrderByDescending(b => b.Id);

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    IsAvailable = b.IsAvailable,
                    AuthorId = b.AuthorId,
                    Author = new AuthorDto
                    {
                        Id = b.Author.Id,
                        FirstName = b.Author.FirstName,
                        LastName = b.Author.LastName,
                        Country = b.Author.Country,
                        DateOfBirth = b.Author.DateOfBirth.HasValue ? 
                            DateOnly.FromDateTime(b.Author.DateOfBirth.Value) : 
                            null,
                    }
                })
                .ToListAsync();

            return new PagedResultDto<BookDto>
            {
                TotalCount = totalCount,
                Items = books
            };
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAll()
                .Include(b => b.Author)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetAll()
                .Include(b => b.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            await _bookRepository.AddAsync(book);

            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateAsync(UpdateBookDto updateBookDto)
        {
            var entity = await _bookRepository.GetAll()
                .AsNoTracking()
                .AnyAsync(a => a.Id == updateBookDto.Id);

            if (!entity)
            {
                throw new Exception($"Book with Id {updateBookDto.Id} not found.");
            }

            var book = _mapper.Map<Book>(updateBookDto);

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                throw new Exception($"Book with Id {id} not found.");
            }

            await _bookRepository.DeleteAsync(book);
        }
    }
}
