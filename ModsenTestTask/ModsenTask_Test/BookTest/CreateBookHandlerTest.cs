using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.BookTest
{
  public class CreateBookHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly CreateBookHandler _handler;

    public CreateBookHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new CreateBookHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorExists()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var command = new CreateBookCommand(authorId, new CreateUpdateBookDTO("12345", "Test Book", "Fiction", null, DateTime.Now, DateTime.Now.AddDays(14)), false);

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync((Author)null);

      // Act & Assert
      await Assert.ThrowsAsync<AuthorNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.CreateBook(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenAuthorExistsAndValidationPasses()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var command = new CreateBookCommand(authorId, new CreateUpdateBookDTO("12345", "Test Book", "Fiction", null, DateTime.Now, DateTime.Now.AddDays(14)), false);
      var author = new Author { Id = authorId };
      var bookEntity = new Book { Id = Guid.NewGuid(), Name = "Test Book" };

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(author);
      _mockRepo.Setup(repo => repo.Book.CreateBook(authorId, It.IsAny<Book>()));
      _mockRepo.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.CreateBook(authorId, It.IsAny<Book>()), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
      Assert.NotNull(result);
      Assert.IsType<BookDTO>(result);
    }
  }
}
