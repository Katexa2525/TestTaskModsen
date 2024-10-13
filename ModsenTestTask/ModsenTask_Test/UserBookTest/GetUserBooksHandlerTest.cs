using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.UserBookTest
{
  public class GetUserBooksHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly GetUserBooksHandler _handler;

    public GetUserBooksHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new GetUserBooksHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsUserBooks_WhenUserBooksExist()
    {
      // Arrange
      var userBooks = new List<UserBook>
        {
            new UserBook {},
            new UserBook {},
        };

      _mockRepo.Setup(repo => repo.UserBook.GetAllUserBooksAsync(false)).ReturnsAsync(userBooks);

      // Act
      var result = await _handler.Handle(new GetUserBooksQuery(false), CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(userBooks.Count, result.Count());
      _mockRepo.Verify(repo => repo.UserBook.GetAllUserBooksAsync(false), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoUserBooksExist()
    {
      // Arrange
      var userBooks = new List<UserBook>();
      _mockRepo.Setup(repo => repo.UserBook.GetAllUserBooksAsync(false)).ReturnsAsync(userBooks);

      // Act
      var result = await _handler.Handle(new GetUserBooksQuery(false), CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Empty(result);
      _mockRepo.Verify(repo => repo.UserBook.GetAllUserBooksAsync(false), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRepositoryFails()
    {
      // Arrange
      _mockRepo.Setup(repo => repo.UserBook.GetAllUserBooksAsync(false))
          .ThrowsAsync(new Exception("Database error"));

      // Act & Assert
      var exception = await Assert.ThrowsAsync<Exception>(() =>
          _handler.Handle(new GetUserBooksQuery(false), CancellationToken.None));

      Assert.Equal("Database error", exception.Message);
      _mockRepo.Verify(repo => repo.UserBook.GetAllUserBooksAsync(false), Times.Once);
    }
  }
}
