using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.AuthorTest
{
  public class GetAuthorByIdHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly GetAuthorByIdHandler _handler;

    public GetAuthorByIdHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new GetAuthorByIdHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsAuthorDTO_WhenAuthorExists()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var author = new Author { Id = authorId, Name = "John", Surname = "Doe" };
      var authorDTO = new AuthorDTO { Id = authorId, Name = "John", Surname = "Doe" };

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, It.IsAny<bool>()))
          .ReturnsAsync(author);

      // Act
      var result = await _handler.Handle(new GetAuthorByIdQuery(authorId, false), CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(authorDTO.Id, result.Id);
      Assert.Equal(authorDTO.Name, result.Name);
      Assert.Equal(authorDTO.Surname, result.Surname);
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
    }
  }
}
