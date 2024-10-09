using Moq;
using Xunit;
using Application.UseCases.Handlers;
using Application.Interfaces.Repository;
using Domain.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Domain.RequestFeatures;
using Application.UseCases.Quaries;
using Domain.Entities;
using Domain.Entities.Models;
using System;

public class GetBooksHandlerTest
{
  [Fact] // Если используете NUnit, то [Test] вместо [Fact]
  public async Task Handle_ReturnsCorrectBookDTOsAndMetaData()
  {
    // Arrange

    // Создаем mock для IRepositoryManager
    var mockRepository = new Mock<IRepositoryManager>();

    // Примерные данные для возврата из репозитория
    var books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                ISBN = "978-3-16-148410-0",
                Name = "Book 1",
                Jenre = "Fiction",
                TakeTime = DateTime.Now.AddDays(-5),
                ReturnTime = DateTime.Now.AddDays(5),
                Author = new Author { Id = Guid.NewGuid(), Name = "Author 1" },
                IdAuthor = Guid.NewGuid(),
                UserBooks = new List<UserBook>()
            },
            new Book
            {
                Id = Guid.NewGuid(),
                ISBN = "978-1-4028-9462-6",
                Name = "Book 2",
                Jenre = "Science",
                TakeTime = DateTime.Now.AddDays(-10),
                ReturnTime = DateTime.Now.AddDays(10),
                Author = new Author { Id = Guid.NewGuid(), Name = "Author 2" },
                IdAuthor = Guid.NewGuid(),
                UserBooks = new List<UserBook>()
            }
        };

    var metaData = new MetaData
    {
      TotalCount = 2,
      PageSize = 10,
      CurrentPage = 1
    };

    // Создаем объект, который будет возвращаться из репозитория
    var booksWithMetaData = new PagedList<Book>(books, 2, 1, 10);

    // Настройка mock-репозитория, чтобы он возвращал эти данные
    mockRepository.Setup(repo => repo.Book.GetAllBooksAsync(It.IsAny<BookParameters>(), It.IsAny<bool>()))
        .ReturnsAsync(booksWithMetaData);

    var handler = new GetBooksHandler(mockRepository.Object);

    var query = new GetBooksQuery(new BookParameters(), false);

    // Act
    var result = await handler.Handle(query, CancellationToken.None);

    // Assert

    // Проверяем, что количество книг в ответе совпадает с ожидаемым
    Assert.Equal(2, result.books.Count());

    // Проверяем метаданные
    Assert.Equal(metaData.TotalCount, result.metaData.TotalCount);
    Assert.Equal(metaData.PageSize, result.metaData.PageSize);
    Assert.Equal(metaData.CurrentPage, result.metaData.CurrentPage);

    // Проверяем первую книгу
    var book1 = result.books.First();
    Assert.Equal("978-3-16-148410-0", book1.ISBN);
    Assert.Equal("Book 1", book1.Name);
    Assert.Equal("Fiction", book1.Jenre);
    Assert.Equal(books[0].TakeTime, book1.TakeTime);
    Assert.Equal(books[0].ReturnTime, book1.ReturnTime);

    // Проверяем вторую книгу
    var book2 = result.books.Last();
    Assert.Equal("978-1-4028-9462-6", book2.ISBN);
    Assert.Equal("Book 2", book2.Name);
    Assert.Equal("Science", book2.Jenre);
    Assert.Equal(books[1].TakeTime, book2.TakeTime);
    Assert.Equal(books[1].ReturnTime, book2.ReturnTime);

    // Проверяем, что репозиторий был вызван один раз
    mockRepository.Verify(repo => repo.Book.GetAllBooksAsync(It.IsAny<BookParameters>(), It.IsAny<bool>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ReturnsEmptyList()
  {
    // Arrange
    var mockRepository = new Mock<IRepositoryManager>();

    var emptyBookList = new PagedList<Book>(new List<Book>(), 0, 1, 10);
    mockRepository.Setup(repo => repo.Book.GetAllBooksAsync(It.IsAny<BookParameters>(), It.IsAny<bool>()))
        .ReturnsAsync(emptyBookList);

    var handler = new GetBooksHandler(mockRepository.Object);
    var query = new GetBooksQuery(new BookParameters(), false);

    // Act
    var result = await handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.Empty(result.books);
    Assert.Equal(0, result.metaData.TotalCount);
  }

  [Fact]
  public async Task Handle_RespectsBookParameters()
  {
    // Arrange
    var mockRepository = new Mock<IRepositoryManager>();
    var books = new List<Book>
    {
        new Book { Id = Guid.NewGuid(), Name = "Book 1", ISBN = "978-3-16-148410-0" },
        new Book { Id = Guid.NewGuid(), Name = "Book 2", ISBN = "978-1-4028-9462-6" }
    };

    var bookParameters = new BookParameters
    {
      PageSize = 1,
      PageNumber = 1
    };

    var booksWithMetaData = new PagedList<Book>(books.Take(1).ToList(), 2, 1, 1);

    mockRepository.Setup(repo => repo.Book.GetAllBooksAsync(bookParameters, It.IsAny<bool>()))
        .ReturnsAsync(booksWithMetaData);

    var handler = new GetBooksHandler(mockRepository.Object);
    var query = new GetBooksQuery(bookParameters, false);

    // Act
    var result = await handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.Single(result.books);
    Assert.Equal("Book 1", result.books.First().Name);
    Assert.Equal(2, result.metaData.TotalCount);
    Assert.Equal(1, result.metaData.PageSize);
  }
}
