using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace ModsenTask_Test.AuthorTest
{
  public class UpdateAuthorHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly UpdateAuthorHandler _handler;

    public UpdateAuthorHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new UpdateAuthorHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ThrowsAuthorNotFoundException_WhenAuthorDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var command = new UpdateAuthorCommand(authorId, new UpdateAuthorDTO("John", "Doe", DateTime.Now, "USA"), false);

      // Настройка mock-а для отсутствующего автора
      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, It.IsAny<bool>()))
          .ReturnsAsync((Author)null);

      // Act & Assert
      var exception = await Assert.ThrowsAsync<AuthorNotFoundException>(() =>
          _handler.Handle(command, CancellationToken.None));

      Assert.Equal($"The author with id: {authorId} doesn't exist in the database.", exception.Message);
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_UpdatesAuthor_WhenValidationSucceeds()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var existingAuthor = new Author { Id = authorId, Name = "Old Name", Surname = "Old Surname", BirthdayDate = DateTime.Now.AddYears(-20), Country = "Old Country" };
      var command = new UpdateAuthorCommand(authorId, new UpdateAuthorDTO("New Name", "New Surname", DateTime.Now, "New Country"), false);

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(existingAuthor);

      var validatorMock = new Mock<IValidator<Author>>();
      validatorMock.Setup(v => v.Validate(It.IsAny<Author>())).Returns(new ValidationResult());

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.Equal(Unit.Value, result);
      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
    }
  }
}
