using Application.Interfaces.Services;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace ModsenTask_Test
{
  public class ProfileUserBookServiceTest
  {
    [Fact]
    public async Task GetAllUserBooksAsyncTest()
    {
      //Arrange
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.UserBookService.GetAllUserBooksAsync(false));

      var controller = new UserBookController(mock.Object);
      //Act
      var result = await controller.GetUserBooks();

      //Assert
      var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
      Assert.NotNull(viewResult);
    }

    [Fact]
    public async Task GetBookByIdTest()
    {
      //Arrange
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.UserBookService.GetUserBookAsync(new Guid("24def6e4-7e78-46a8-b197-864a41dae62d"),
                                                        "c10313f5-094d-4b38-a345-e96da4b63336", false));

      var controller = new UserBookController(mock.Object);
      //Act
      var result = await controller.GetUserBookById(new Guid("24def6e4-7e78-46a8-b197-864a41dae62d"),
                                                   "c10313f5-094d-4b38-a345-e96da4b63336");
      var resultIsNull = await controller.GetUserBookById(new Guid("24def6e4-7e78-46a8-b197-864a41dae62d"),
                                                         "c10313f5-094d-4b38-a345-e96da4b63336");

      //Assert
      var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
      Assert.NotNull(viewResult);
      Assert.IsNotType<UserBookDTO>(resultIsNull);
    }
  }
}
