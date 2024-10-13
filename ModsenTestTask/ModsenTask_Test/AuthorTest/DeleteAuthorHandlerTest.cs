using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Application.Exceptions;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.AuthorTest
{
  public class DeleteAuthorHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly DeleteAuthorHandler _handler;

    public DeleteAuthorHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new DeleteAuthorHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ThrowsAuthorNotFoundException_WhenAuthorDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, It.IsAny<bool>()))
          .ReturnsAsync((Author)null);

      // Act & Assert
      var exception = await Assert.ThrowsAsync<AuthorNotFoundException>(() =>
          _handler.Handle(new DeleteAuthorCommand(authorId, false), CancellationToken.None));

      Assert.Equal($"The author with id: {authorId} doesn't exist in the database.", exception.Message); 
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Author.DeleteAuthor(It.IsAny<Author>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_DeletesAuthor_WhenAuthorExists()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var author = new Author { Id = authorId };
      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(author);

      // Act
      await _handler.Handle(new DeleteAuthorCommand(authorId, false), CancellationToken.None);

      // Assert
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Author.DeleteAuthor(author), Times.Once); 
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once); 
    }
  }
}
