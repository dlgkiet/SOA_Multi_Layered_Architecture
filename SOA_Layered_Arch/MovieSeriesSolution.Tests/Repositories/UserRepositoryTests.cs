using Moq;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieSeriesSolution.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly Mock<IRepository<User>> _repositoryMock;

        public UserRepositoryTests()
        {
            _repositoryMock = new Mock<IRepository<User>>();
        }

        [Fact]
        public async Task AddAsync_ShouldCallAddAsync_WhenUserIsValid()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com"
            };

            _repositoryMock.Setup(repo => repo.AddAsync(user, default))
                .ReturnsAsync(user);

            // Act
            var result = await _repositoryMock.Object.AddAsync(user);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(user, default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "user1", Email = "user1@example.com" },
                new User { Id = 2, Username = "user2", Email = "user2@example.com" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync(default))
                .ReturnsAsync(users);

            // Act
            var result = await _repositoryMock.Object.GetAllAsync();

            // Assert
            Assert.Equal(users, result);
            Assert.Equal(2, result.Count()); // Kiểm tra số lượng phần tử
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "testuser", Email = "test@example.com" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1, default))
                .ReturnsAsync(user);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetByIdAsync(999, default))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "UpdatedUser", Email = "updated@example.com" };

            _repositoryMock.Setup(repo => repo.UpdateAsync(user, default))
                .ReturnsAsync(user);

            // Act
            var result = await _repositoryMock.Object.UpdateAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new User { Id = 999, Username = "NonExistentUser", Email = "nonexistent@example.com" };

            _repositoryMock.Setup(repo => repo.UpdateAsync(user, default))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _repositoryMock.Object.UpdateAsync(user);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(1, default))
                .ReturnsAsync(true);

            // Act
            var result = await _repositoryMock.Object.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(999, default))
                .ReturnsAsync(false);

            // Act
            var result = await _repositoryMock.Object.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}
