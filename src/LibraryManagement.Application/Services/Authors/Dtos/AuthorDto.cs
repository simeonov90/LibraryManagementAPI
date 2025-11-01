using LibraryManagement.Application.Services.Books.Dtos;

namespace LibraryManagement.Application.Services.Authors.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string? Country { get; set; }

        public ICollection<BookDto> Books { get; set; }
    }
}
