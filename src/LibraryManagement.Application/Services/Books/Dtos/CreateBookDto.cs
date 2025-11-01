using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Services.Books.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public int? Year { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required]
        public int AuthorId { get; set; }
    }
}
