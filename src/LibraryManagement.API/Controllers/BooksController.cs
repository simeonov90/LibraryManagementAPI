using LibraryManagement.API.Extensions;
using LibraryManagement.Application.Services.Books;
using LibraryManagement.Application.Services.Books.Dtos;
using LibraryManagement.Application.Services.Shared.Dtos;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<BookDto>>> GetBooks([FromQuery] BookFilterDto filter)
        {
            var pagedBooks = await _bookService.GetPagedResultAsync(filter);
            return Ok(pagedBooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

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

            var book = await _bookService.CreateAsync(createBookDto);

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
                await _bookService.UpdateAsync(updateBookDto);
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
                await _bookService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }

            return NoContent();
        }
    }
}
