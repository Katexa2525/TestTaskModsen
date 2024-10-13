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
      _mockRepo = new Mock<IRepositoryManager>();

      _handler = new UpdateBookHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();

      var command = new UpdateBookCommand(authorId, bookId,
          new CreateUpdateBookDTO(
              ISBN: "978-3-16-148410-0", 
              Name: "Test Book",       
              Jenre: "Fiction",        
              Image: Mock.Of<IFormFile>(), 
              TakeTime: DateTime.Now,  
              ReturnTime: DateTime.Now.AddDays(7) 
          ),
          authTrackChanges: false,
          bookTrackChanges: false
      );

      _mockRepo.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync((Author)null);

      // Act & Assert
      await Assert.ThrowsAsync<AuthorNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

      _mockRepo.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepo.Verify(repo => repo.Book.GetBookByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()), Times.Never);
      _mockRepo.Verify(repo => repo.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExistWithException()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();
      var command = new UpdateBookCommand(authorId, bookId,
          new CreateUpdateBookDTO(
              ISBN: "978-3-16-148410-0", 
              Name: "Test Book",       
              Jenre: "Fiction",        
              Image: Mock.Of<IFormFile>(), 
              TakeTime: DateTime.Now,  
              ReturnTime: DateTime.Now.AddDays(7) 
          ),
          authTrackChanges: false,
          bookTrackChanges: false
      );

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
