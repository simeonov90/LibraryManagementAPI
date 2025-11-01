using LibraryManagement.Application.Services.Authors.Dtos;

namespace LibraryManagement.Application.Services.Books.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? Year { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }
    }
}
