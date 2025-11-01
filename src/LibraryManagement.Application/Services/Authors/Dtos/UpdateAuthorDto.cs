using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Services.Authors.Dtos
{
    public class UpdateAuthorDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string? Country { get; set; }
    }
}
