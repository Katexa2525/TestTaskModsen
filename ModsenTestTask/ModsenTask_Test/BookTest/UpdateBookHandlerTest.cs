using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ModsenTask_Test.BookTest
{
  public class UpdateBookHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private UpdateBookHandler _handler;

    public UpdateBookHandlerTest()
    {
      // Создаем mock для IRepositoryManager
      _mockRepo = new Mock<IRepositoryManager>();

      // Передаем mock в handler
      _handler = new UpdateBookHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ThrowsAuthorNotFoundException_WhenAuthorDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();

      // Создание DTO с тестовыми данными
      var command = new UpdateBookCommand(authorId, bookId,
          new CreateUpdateBookDTO(
              ISBN: "978-3-16-148410-0", // Пример ISBN
              Name: "Test Book",        // Пример Названия книги
              Jenre: "Fiction",        // Пример Жанра
              Image: Mock.Of<IFormFile>(), // Замена на мок файл (Mock<IFormFile>)
              TakeTime: DateTime.Now,  // Пример Время взятия
              ReturnTime: DateTime.Now.AddDays(7) // Пример Время возврата
          ),
          authTrackChanges: false,
          bookTrackChanges: false
      );

      // Настройка mock-а для отсутствующего автора
      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync((Author)null);

      // Act & Assert
      await Assert.ThrowsAsync<AuthorNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

      // Проверяем вызовы репозитория
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.GetBookByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ThrowsBookNotFoundException_WhenBookDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();

      // Создание DTO с тестовыми данными
      var command = new UpdateBookCommand(authorId, bookId,
          new CreateUpdateBookDTO(
              ISBN: "978-3-16-148410-0", // Пример ISBN
              Name: "Test Book",        // Пример Названия книги
              Jenre: "Fiction",        // Пример Жанра
              Image: Mock.Of<IFormFile>(), // Замена на мок файл
              TakeTime: DateTime.Now,  // Пример Время взятия
              ReturnTime: DateTime.Now.AddDays(7) // Пример Время возврата
          ),
          authTrackChanges: false,
          bookTrackChanges: false
      );

      // Mock-им существование автора, но отсутствие книги
      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(new Author { Id = authorId });
      _mockRepo.Setup(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false))
          .ReturnsAsync((Book)null);

      // Act & Assert
      await Assert.ThrowsAsync<BookNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }
  }
}
