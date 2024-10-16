﻿using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.UseCases.Handlers;
using Application.Exceptions;
using Domain.Entities.Models;
using MediatR;
using Moq;

namespace ModsenTask_Test.UserBookTest
{
  public class DeleteUserBookByIdHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly DeleteUserBookByIdHandler _handler;

    public DeleteUserBookByIdHandlerTest()
    {
      _mockRepo = new Mock<IRepositoryManager>();
      _handler = new DeleteUserBookByIdHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenUserBookExists()
    {
      // Arrange
      var userBookId = Guid.NewGuid();
      var userBook = new UserBook { Id = userBookId };

      _mockRepo.Setup(repo => repo.UserBook.GetUserBookByUBId(userBookId, It.IsAny<bool>()))
          .ReturnsAsync(userBook);
      _mockRepo.Setup(repo => repo.UserBook.DeleteUserBook(userBook));

      // Act
      var result = await _handler.Handle(new DeleteUserBookByIdCommand(userBookId, false), CancellationToken.None);

      // Assert
      Assert.Equal(Unit.Value, result);
      _mockRepo.Verify(repo => repo.UserBook.GetUserBookByUBId(userBookId, false), Times.Once);
      _mockRepo.Verify(repo => repo.UserBook.DeleteUserBook(userBook), Times.Once);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist()
    {
      // Arrange
      var userBookId = Guid.NewGuid();

      _mockRepo.Setup(repo => repo.UserBook.GetUserBookByUBId(userBookId, It.IsAny<bool>()))
          .ReturnsAsync((UserBook)null);

      // Act & Assert
      var exception = await Assert.ThrowsAsync<UserBookNotFoundException>(() =>
          _handler.Handle(new DeleteUserBookByIdCommand(userBookId, false), CancellationToken.None));

      Assert.Equal($"The userbook with bookid: {userBookId} doesn't exist in the database.", exception.Message);
      _mockRepo.Verify(repo => repo.UserBook.GetUserBookByUBId(userBookId, false), Times.Once);
      _mockRepo.Verify(repo => repo.UserBook.DeleteUserBook(It.IsAny<UserBook>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }
  }
}
