using Contracts.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using ModsenTestTask.Controllers;
using Moq;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask_Test
{
  public class ProfileBookServiceTest
  {
    [Fact]
    public async Task GetAllBooksAsyncTest()
    {
      //Arrange
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.BookService.GetAllBooksAsync(new BookParameters(), false));

      var controller = new BooksController(mock.Object);
      //Act
      var result = await controller.GetAllBooks(new BookParameters());

      //Assert
      var notFound = Assert.IsType<BadRequestObjectResult>(result);
      Assert.Equal("pagedResultMeta object is null", notFound.Value);
    }

    [Fact]
    public async Task GetBookByIdTest()
    {
      //Arrange
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.BookService.GetBookByIdAsync(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc9"), 
                                                     new Guid("24def6e4-7e78-46a8-b197-864a41dae62d"),false));

      var controller = new BooksController(mock.Object);
      //Act
      var result = await controller.GetBookById(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc9"), 
                                                new Guid("24def6e4-7e78-46a8-b197-864a41dae62d"));
      var resultIsNull = await controller.GetBookById(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc8"),
                                                      new Guid("24def6e4-7e78-46a8-b197-864a41dae62d"));

      //Assert
      var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
      Assert.NotNull(viewResult);
      Assert.IsNotType<BookDTO>(resultIsNull);
    }

    [Fact]
    public async Task CreateBookTest()
    {
      CreateUpdateBookDTO createBookDTO = new CreateUpdateBookDTO("1234", "TestName", "Comedy", new DateTime(1977, 07, 29),
                                                                  new DateTime(1977, 08, 29));

      //Arrange
      Guid testId = new Guid("24def6e4-7e78-46a8-b197-864a41dae62d");
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.BookService.CreateBookAsync(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc8"), createBookDTO, true));
      var controller = new BooksController(mock.Object);
      //Act
      var result = await controller.CreateBook(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc8"), createBookDTO);

      //Assert
      var notFound = Assert.IsType<BadRequestObjectResult>(result);
      Assert.NotEqual(testId, notFound.Value);
      Assert.Equal("bookToReturn object is null", notFound.Value);
    }
  }
}
