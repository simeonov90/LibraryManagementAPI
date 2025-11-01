using AutoMapper;
using LibraryManagement.Application.Services.Authors.Dtos;
using LibraryManagement.Domain.IRepositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Services.Authors
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetAll()
                .Include(a => a.Books)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return author == null ? null : _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto createAuthorDto)
        {
            var author = _mapper.Map<Author>(createAuthorDto);
            await _authorRepository.AddAsync(author);

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAsync(UpdateAuthorDto updateAuthorDto)
        {
            var entity = await _authorRepository.GetAll()
                .AsNoTracking()
                .AnyAsync(a => a.Id == updateAuthorDto.Id);

            if (!entity)
            {
                throw new Exception($"Author with Id {updateAuthorDto.Id} not found.");
            }

            var author = _mapper.Map<Author>(updateAuthorDto);

            await _authorRepository.UpdateAsync(author);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                throw new Exception($"Author with Id {id} not found.");
            }

            await _authorRepository.DeleteAsync(author);
        }
    }
}
