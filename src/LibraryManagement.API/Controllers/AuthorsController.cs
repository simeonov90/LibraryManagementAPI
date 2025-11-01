using LibraryManagement.API.Extensions;
using LibraryManagement.Application.Services.Authors;
using LibraryManagement.Application.Services.Authors.Dtos;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAsync();

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
            {
                throw new NotFoundException(nameof(Author), id);
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                throw new ValidationException(errors);
            }

            var author = await _authorService.CreateAsync(createAuthorDto);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            if (id != updateAuthorDto.Id)
            {
                throw new BadRequestException("Author ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                throw new ValidationException(errors);
            }

            try
            {
                await _authorService.UpdateAsync(updateAuthorDto);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }

            return NoContent();
        }
    }
}
