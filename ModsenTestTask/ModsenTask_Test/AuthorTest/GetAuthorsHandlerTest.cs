using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.AuthorTest
{
  public class GetAuthorsHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly GetAuthorsHandler _handler;

    public GetAuthorsHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new GetAuthorsHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorsExist()
    {
      // Arrange
      var authors = new List<Author>
        {
            new Author { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" },
            new Author { Id = Guid.NewGuid(), Name = "Jane", Surname = "Doe" }
        };

      _mockRepo.Setup(repo => repo.Author.GetAllAuthorsAsync(It.IsAny<bool>()))
          .ReturnsAsync(authors);

      // Act
      var result = await _handler.Handle(new GetAuthorsQuery(false), CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(authors.Count, result.Count());
      _mockRepo.Verify(repo => repo.Author.GetAllAuthorsAsync(false), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenAuthorExists()
    {
      // Arrange
      _mockRepo.Setup(repo => repo.Author.GetAllAuthorsAsync(It.IsAny<bool>()))
          .ReturnsAsync(new List<Author>());

      // Act
      var result = await _handler.Handle(new GetAuthorsQuery(false), CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Empty(result);
      _mockRepo.Verify(repo => repo.Author.GetAllAuthorsAsync(false), Times.Once);
    }
  }
}
