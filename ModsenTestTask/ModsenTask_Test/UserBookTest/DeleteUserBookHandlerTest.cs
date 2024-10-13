using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ModsenTask_Test.UserBookTest
{
  public class DeleteUserBookHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly Mock<IUserStore<User>> _mockUserStore;
    private readonly Mock<UserManager<User>> _mockUserManager;
    private readonly DeleteUserBookHandler _handler;

    public DeleteUserBookHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();

      // Создаем Mock для IUserStore
      _mockUserStore = new Mock<IUserStore<User>>();
      _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object, null, null, null, null, null, null, null, null);

      _handler = new DeleteUserBookHandler(_mockRepo.Object, _mockUserManager.Object);
    }

    [Fact]
    public async Task Handle_DeletesUserBook_WhenUserBookExists()
    {
      // Arrange
      var userName = "testUser";
      var userId = Guid.NewGuid().ToString();
      var bookId = Guid.NewGuid();
      var userBook = new UserBook { Id = bookId, IdUser = userId /* другие свойства */ };

      var user = new User { Id = userId, UserName = userName };

      // Настройка мока для пользователя
      _mockUserManager.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync(user);

      // Настройка мока для существующей книги
      _mockRepo.Setup(repo => repo.UserBook.GetUserBookByIdAsync(bookId, userId, It.IsAny<bool>()))
          .ReturnsAsync(userBook);

      // Настройка метода удаления
      _mockRepo.Setup(repo => repo.UserBook.DeleteUserBook(userBook));

      // Act
      var result = await _handler.Handle(new DeleteUserBookCommand(bookId, userName, false), CancellationToken.None);

      // Assert
      Assert.Equal(Unit.Value, result);
      _mockRepo.Verify(repo => repo.UserBook.GetUserBookByIdAsync(bookId, userId, false), Times.Once);
      _mockRepo.Verify(repo => repo.UserBook.DeleteUserBook(userBook), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsUserBookNotFoundException_WhenUserBookDoesNotExist()
    {
      // Arrange
      var userName = "nonExistingUser";
      var userId = Guid.NewGuid().ToString();
      var bookId = Guid.NewGuid();

      var user = new User { Id = userId, UserName = userName };

      // Настройка мока для пользователя
      _mockUserManager.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync(user);

      // Настройка мока для отсутствующей книги
      _mockRepo.Setup(repo => repo.UserBook.GetUserBookByIdAsync(bookId, userId, It.IsAny<bool>()))
          .ReturnsAsync((UserBook)null);

      // Act & Assert
      var exception = await Assert.ThrowsAsync<UserBookNotFoundException>(() =>
          _handler.Handle(new DeleteUserBookCommand(bookId, userName, false), CancellationToken.None));

      Assert.Equal($"The userbook with bookid: {bookId} doesn't exist in the database.", exception.Message); // Предполагая, что у UserBookNotFoundException есть свойство IdUserBook
      _mockRepo.Verify(repo => repo.UserBook.GetUserBookByIdAsync(bookId, userId, false), Times.Once);
      _mockRepo.Verify(repo => repo.UserBook.DeleteUserBook(It.IsAny<UserBook>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }
  }
}
