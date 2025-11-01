using LibraryManagement.API.Extensions;
using LibraryManagement.Application.Services.Books;
using LibraryManagement.Application.Services.Books.Dtos;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookServices _bookServices;

        public BooksController(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookServices.GetAllAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookServices.GetByIdAsync(id);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), id);
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                throw new ValidationException(errors);
            }

            var book = await _bookServices.CreateAsync(createBookDto);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (id != updateBookDto.Id)
            {
                throw new BadRequestException("Book ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                throw new ValidationException(errors);
            }

            try
            {
                await _bookServices.UpdateAsync(updateBookDto);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookServices.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }

            return NoContent();
        }
    }
}
