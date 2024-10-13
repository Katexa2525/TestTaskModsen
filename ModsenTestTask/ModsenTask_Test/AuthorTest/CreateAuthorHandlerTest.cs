using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Domain.Entities.Validation;
using Moq;

namespace ModsenTask_Test.AuthorTest
{
  public class CreateAuthorHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly CreateAuthorHandler _handler;

    public CreateAuthorHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new CreateAuthorHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_CreatesAuthor_WhenValidationSucceeds()
    {
      // Arrange
      var newAuthor = new CreateAuthorDTO("John", "Doe", new DateTime(1990, 1, 1), "USA");
      var command = new CreateAuthorCommand(newAuthor);
      var authorEntity = new Author
      {
        Name = newAuthor.Name,
        Surname = newAuthor.Surname,
        BirthdayDate = newAuthor.BirthdayDate,
        Country = newAuthor.Country
      };

      // Настройка mock-а для успешной валидации
      var validator = new AuthorValidator(); // Используйте реальный валидатор
      var validationResult = validator.Validate(authorEntity);
      Assert.True(validationResult.IsValid); // Убедитесь, что валидатор возвращает успешный результат

      // Act
      _mockRepo.Setup(repo => repo.Author.CreateAuthor(It.IsAny<Author>())).Callback<Author>(a =>
      {
        // Проверяем, что созданный автор соответствует ожиданиям
        Assert.Equal(authorEntity.Name, a.Name);
        Assert.Equal(authorEntity.Surname, a.Surname);
        Assert.Equal(authorEntity.BirthdayDate, a.BirthdayDate);
        Assert.Equal(authorEntity.Country, a.Country);
      });

      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(newAuthor.Name, result.Name);
      Assert.Equal(newAuthor.Surname, result.Surname);
      _mockRepo.Verify(repo => repo.Author.CreateAuthor(It.IsAny<Author>()), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenValidationFails()
    {
      // Arrange
      var newAuthor = new CreateAuthorDTO("", "Doe", new DateTime(1990, 1, 1), "USA"); // Неправильное имя
      var command = new CreateAuthorCommand(newAuthor);
      var authorEntity = new Author
      {
        Name = newAuthor.Name,
        Surname = newAuthor.Surname,
        BirthdayDate = newAuthor.BirthdayDate,
        Country = newAuthor.Country
      };

      // Настройка mock-а для провала валидации
      var validator = new AuthorValidator(); // Используйте реальный валидатор
      var validationResult = validator.Validate(authorEntity);
      Assert.False(validationResult.IsValid); // Убедитесь, что валидатор возвращает неуспешный результат

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.Null(result);
      _mockRepo.Verify(repo => repo.Author.CreateAuthor(It.IsAny<Author>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }
  }
}
