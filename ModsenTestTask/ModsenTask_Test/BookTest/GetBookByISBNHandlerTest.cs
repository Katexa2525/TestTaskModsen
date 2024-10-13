using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Application.Exceptions;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.BookTest
{
  public class GetBookByISBNHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly GetBookByISBNHandler _handler;

    public GetBookByISBNHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();

      _handler = new GetBookByISBNHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenBookExists()
    {
      // Arrange
      var bookISBN = "123456789";
      var query = new GetBookByISBNQuery(bookISBN, trackChanges: false);
      _mockRepo.Setup(repo => repo.Book.GetBookByISBNAsync(bookISBN, false))
          .ReturnsAsync(new Book
          {
            ISBN = bookISBN,
            Name = "Test Book"
          });

      // Act
      var result = await _handler.Handle(query, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(bookISBN, result.ISBN);
      _mockRepo.Verify(repo => repo.Book.GetBookByISBNAsync(bookISBN, false), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist()
    {
      // Arrange
      var bookISBN = "987654321";
      var query = new GetBookByISBNQuery(bookISBN, trackChanges: false);
      _mockRepo.Setup(repo => repo.Book.GetBookByISBNAsync(bookISBN, false)).ReturnsAsync((Book)null);

      // Act & Assert
      await Assert.ThrowsAsync<BookNotFoundByISBN>(() => _handler.Handle(query, CancellationToken.None));

      _mockRepo.Verify(repo => repo.Book.GetBookByISBNAsync(bookISBN, false), Times.Once);
    }
  }
}
