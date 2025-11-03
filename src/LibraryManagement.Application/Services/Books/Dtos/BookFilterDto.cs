namespace LibraryManagement.Application.Services.Books.Dtos
{
    public class BookFilterDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string SortOrder { get; set; } = "desc";
    }
}
