using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Application.Exceptions;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ModsenTask_Test.UserBookTest
{
    public class GetUserBookByIdHandlerTest
    {
        private readonly Mock<IRepositoryManager> _mockRepository;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly GetUserBookByIdHandler _handler;
        bool trackChanges = false;

        public GetUserBookByIdHandlerTest()
        {
            _mockRepository = new Mock<IRepositoryManager>();
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _handler = new GetUserBookByIdHandler(_mockRepository.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task Handle_WhenBookDoesNotExist()
        {
            // Arrange
            var userName = "testuser";
            var bookId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = userName };
            var userBook = new UserBook { IdBook = bookId, IdUser = user.Id };
            _mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);

            _mockRepository.Setup(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()))
                .ReturnsAsync(userBook);

            var query = new GetUserBookByIdQuery(bookId, userName, trackChanges);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.IdBook);
            Assert.Equal(user.Id, result.IdUser);

            _mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            _mockRepository.Verify(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenBookDoesNotExistWithException()
        {
            // Arrange
            var userName = "testuser";
            var bookId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = userName };

            _mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);

            _mockRepository.Setup(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()))
                .ReturnsAsync((UserBook)null);

            var query = new GetUserBookByIdQuery(bookId, userName, trackChanges);

            // Act & Assert
            await Assert.ThrowsAsync<UserBookNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            _mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            _mockRepository.Verify(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()), Times.Once);
        }
    }
}
