using AutoMapper;
using LibraryManagement.Application.Services.Books.Dtos;
using LibraryManagement.Domain.IRepositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Services.Books
{
    public class BookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookServices(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task CreateAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateAsync(BookDto bookDto)
        {
            var entity = await _bookRepository.GetAll()
                .AsNoTracking()
                .AnyAsync(a => a.Id == bookDto.Id);

            if (!entity)
            {
                throw new Exception($"Book with Id {bookDto.Id} not found.");
            }

            var book = _mapper.Map<Book>(bookDto);

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
