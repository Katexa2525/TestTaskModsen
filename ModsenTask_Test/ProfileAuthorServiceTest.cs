using Application.Interfaces.Services;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace ModsenTask_Test
{
  public class ProfileAuthorServiceTest
  {

    [Fact]
    public async Task GetAllAuthorsAsyncTest()
    {
      //Arrange
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.AuthorService.GetAllAuthorsAsync(false));

      var controller = new AuthorsController(mock.Object);
      //Act
      var result = await controller.GetAuthors();

      //Assert
      var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
      Assert.NotNull(viewResult);
    }

    [Fact]
    public async Task GetAuthorByIdTest()
    {
      //Arrange
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.AuthorService.GetAuthorAsync(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc9"), false));

      var controller = new AuthorsController(mock.Object);
      //Act
      var result = await controller.GetAuthorById(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc9"));
      var resultIsNull = await controller.GetAuthorById(new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc8"));

      //Assert
      var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
      Assert.NotNull(viewResult);
      Assert.IsNotType<AuthorDTO>(resultIsNull);
    }

    [Fact]
    public async Task CreateAuthorTest()
    {
      CreateAuthorDTO createAuthorDTO = new CreateAuthorDTO("TestName", "TestSurname", new DateTime(1977,07,29), "RB");

      //Arrange
      Guid testId = new Guid("b45f57f5-9ac0-4570-aa21-dfd1bcf04cc9"); 
      var mock = new Mock<IServiceManager>();
      mock.Setup(s => s.AuthorService.CreateAuthorAsync(createAuthorDTO));
      var controller = new AuthorsController(mock.Object);
      //Act
      var result = await controller.CreateAuthor(createAuthorDTO);

      //Assert
      var notFound = Assert.IsType<BadRequestObjectResult>(result);
      Assert.NotEqual(testId, notFound.Value); 
      Assert.Equal("AuthorDTO object is null", notFound.Value);
    }
  }
}