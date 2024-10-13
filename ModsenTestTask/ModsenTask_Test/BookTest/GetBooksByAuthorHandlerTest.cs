using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Application.Exceptions;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.BookTest
{
  public class GetBooksByAuthorHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly GetBooksByAuthorHandler _handler;

    public GetBooksByAuthorHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new GetBooksByAuthorHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsBooks_WhenAuthorExists()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var books = new List<Book>
        {
            new Book { Id = Guid.NewGuid(), Name = "Book 1" },
            new Book { Id = Guid.NewGuid(), Name = "Book 2" }
        };
      var query = new GetBooksByAuthorQuery(authorId, false);

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(new Author { Id = authorId, Name = "Test Author" });

      _mockRepo.Setup(repo => repo.Book.GetBookByAuthorAsync(authorId, false))
          .ReturnsAsync(books);

      // Act
      var result = await _handler.Handle(query, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, result.Count());
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.GetBookByAuthorAsync(authorId, false), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsAuthorNotFoundException_WhenAuthorDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var query = new GetBooksByAuthorQuery(authorId, false);

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync((Author)null);

      // Act & Assert
      await Assert.ThrowsAsync<AuthorNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.GetBookByAuthorAsync(authorId, false), Times.Never); 
    }
  }
}
