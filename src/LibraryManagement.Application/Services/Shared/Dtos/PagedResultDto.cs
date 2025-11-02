namespace LibraryManagement.Application.Services.Shared.Dtos
{
    public class PagedResultDto<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
