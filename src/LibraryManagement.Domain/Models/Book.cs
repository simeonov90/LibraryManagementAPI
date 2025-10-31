namespace LibraryManagement.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}