using Application.Interfaces.Repository;
using Application.UseCases.Handlers;
using Application.UseCases.Quaries;
using Domain.Entities.Exceptions;
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

            // Mock UserManager with required parameters
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            // Initialize the handler
            _handler = new GetUserBookByIdHandler(_mockRepository.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task Handle_ReturnsUserBookDTO_WhenUserAndBookExist()
        {
            // Arrange
            var userName = "testuser";
            var bookId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = userName };
            var userBook = new UserBook { IdBook = bookId, IdUser = user.Id };

            // Настройка mock для UserManager: возвращаем пользователя
            _mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);

            // Настройка mock для UserBook репозитория: возвращаем книгу пользователя
            _mockRepository.Setup(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()))
                .ReturnsAsync(userBook);

            var query = new GetUserBookByIdQuery(bookId, userName, trackChanges);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.IdBook);
            Assert.Equal(user.Id, result.IdUser);

            // Проверяем, что методы были вызваны
            _mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            _mockRepository.Verify(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsUserBookNotFoundException_WhenBookDoesNotExist()
        {
            // Arrange
            var userName = "testuser";
            var bookId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = userName };

            // Настройка mock для UserManager: возвращаем пользователя
            _mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);

            // Настройка mock для UserBook репозитория: возвращаем null, чтобы сымитировать отсутствие книги
            _mockRepository.Setup(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()))
                .ReturnsAsync((UserBook)null);

            var query = new GetUserBookByIdQuery(bookId, userName, trackChanges);

            // Act & Assert
            await Assert.ThrowsAsync<UserBookNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            // Проверяем, что методы были вызваны
            _mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            _mockRepository.Verify(repo => repo.UserBook.GetUserBookByIdAsync(bookId, user.Id, It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userName = "nonexistentuser";
            var bookId = Guid.NewGuid();

            // Настройка mock для UserManager: возвращаем null, чтобы сымитировать отсутствие пользователя
            _mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync((User)null);

            var query = new GetUserBookByIdQuery(bookId, userName, trackChanges);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);

            // Проверяем, что FindByNameAsync был вызван один раз
            _mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);

            // Проверяем, что метод репозитория не был вызван, так как пользователь не найден
            _mockRepository.Verify(repo => repo.UserBook.GetUserBookByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>().ToString(), It.IsAny<bool>()), Times.Never);
        }
    }
}
