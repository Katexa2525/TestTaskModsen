using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.BookTest
{
  public class DeleteBookHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly DeleteBookHandler _handler;

    public DeleteBookHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new DeleteBookHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ThrowsBookNotFoundException_WhenBookDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();
      var command = new DeleteBookCommand(authorId, bookId, false);

      // Настройка mock-а для отсутствующей книги
      _mockRepo.Setup(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false))
          .ReturnsAsync((Book)null);

      // Act & Assert
      await Assert.ThrowsAsync<BookNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

      _mockRepo.Verify(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.DeleteBook(It.IsAny<Book>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_DeletesBook_WhenBookExists()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();
      var book = new Book { Id = bookId, IdAuthor = authorId };

      var command = new DeleteBookCommand(authorId, bookId, false);

      // Настройка mock-а для существующей книги
      _mockRepo.Setup(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false))
          .ReturnsAsync(book);

      // Act
      await _handler.Handle(command, CancellationToken.None);

      // Assert
      _mockRepo.Verify(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.DeleteBook(book), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
    }
  }
}
