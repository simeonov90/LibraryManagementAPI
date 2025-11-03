using AutoMapper;
using LibraryManagement.Application.Services.Authors.Dtos;
using LibraryManagement.Domain.Models;
using MockQueryable.Moq;

namespace LibraryManagement.Tests
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _authorService = new AuthorService(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAuthor_WhenAuthorExist()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Test", LastName = "Author", Books = new List<Book>() }
            };

            var mockQueryable = authors.AsQueryable().BuildMock();

            _authorRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(mockQueryable);

            var expectedDto = new AuthorDto { Id = 1, FirstName = "Test", LastName = "Author" };

            _mapperMock
                .Setup(m => m.Map<AuthorDto>(It.IsAny<Author>()))
                .Returns(expectedDto);

            // Act
            var result = await _authorService.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be("Test");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Test", LastName = "Author", Books = new List<Book>() }
            };

            var mockQueryable = authors.AsQueryable().BuildMock();

            _authorRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(mockQueryable);

            var expectedDto = new AuthorDto { Id = 1, FirstName = "Test", LastName = "Author" };

            _mapperMock
                .Setup(m => m.Map<AuthorDto>(It.IsAny<Author>()))
                .Returns(expectedDto);

            // Act
            var result = await _authorService.GetByIdAsync(2);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenCantFindEntity()
        {
            // Arrange
            var updateAuthorDto = new UpdateAuthorDto
            {
                Id = 999,
                FirstName = "NonExistent",
                LastName = "Author"
            };

            var authors = new List<Author>(); // Empty list - no authors exist

            var mockQueryable = authors.AsQueryable().BuildMock();

            _authorRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(mockQueryable);

            // Act
            Func<Task> act = async () => await _authorService.UpdateAsync(updateAuthorDto);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Author with Id 999 not found.");
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            int authorId = 999;

            _authorRepositoryMock
                .Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync((Author?)null);

            // Act
            Func<Task> act = async () => await _authorService.DeleteAsync(authorId);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Author with Id 999 not found.");
        }
    }
}
