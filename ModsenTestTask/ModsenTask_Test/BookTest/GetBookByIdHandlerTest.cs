﻿using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using Moq;

namespace ModsenTask_Test.BookTest
{
  public class GetBookByIdHandlerTest
  {
    private readonly Mock<IRepositoryManager> _mockRepository;
    private readonly GetBookByIdHandler _handler;

    public GetBookByIdHandlerTest()
    {
      _mockRepository = new Mock<IRepositoryManager>();
      _handler = new GetBookByIdHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorExists()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();
      var request = new GetBookByIdQuery (authorId, bookId, false);

      var author = new Author { Id = authorId };
      var book = new Book { Id = bookId };

      _mockRepository.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(author);

      _mockRepository.Setup(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false))
          .ReturnsAsync(book);

      // Act
      var result = await _handler.Handle(request, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.IsType<BookDTO>(result);
      _mockRepository.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepository.Verify(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenAuthorDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();
      var request = new GetBookByIdQuery (authorId, bookId, false);

      _mockRepository.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync((Author)null);

      // Act & Assert
      await Assert.ThrowsAsync<AuthorNotFoundException>(async () =>
          await _handler.Handle(request, CancellationToken.None));

      _mockRepository.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepository.Verify(repo => repo.Book.GetBookByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist()
    {
      // Arrange
      var authorId = Guid.NewGuid();
      var bookId = Guid.NewGuid();
      var request = new GetBookByIdQuery (authorId, bookId, false);

      var author = new Author { Id = authorId };

      _mockRepository.Setup(repo => repo.Author.GetAuthorByIdAsync(authorId, false))
          .ReturnsAsync(author); 

      _mockRepository.Setup(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false))
          .ReturnsAsync((Book)null);

      // Act & Assert
      await Assert.ThrowsAsync<BookNotFoundException>(async () =>
          await _handler.Handle(request, CancellationToken.None));

      _mockRepository.Verify(repo => repo.Author.GetAuthorByIdAsync(authorId, false), Times.Once);
      _mockRepository.Verify(repo => repo.Book.GetBookByIdAsync(authorId, bookId, false), Times.Once);
    }
  }
}
