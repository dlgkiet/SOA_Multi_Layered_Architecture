using Moq;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using SOA_Layered_Arch.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieSeriesSolution.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _repositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _repositoryMock = new Mock<IRepository<User>>();
            _userService = new UserService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldCallRepositoryAddAsync()
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
            var result = await _userService.AddUserAsync(user);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(user, default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnListOfUsers()
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
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Equal(users, result);
            Assert.Equal(2, result.Count()); // Kiểm tra số lượng phần tử
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "testuser", Email = "test@example.com" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1, default))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetByIdAsync(999, default))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.GetUserByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldThrowException_WhenIdIsInvalid()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserByIdAsync(0));
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "UpdatedUser", Email = "updated@example.com" };

            _repositoryMock.Setup(repo => repo.UpdateAsync(user, default))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new User { Id = 999, Username = "NonExistentUser", Email = "nonexistent@example.com" };

            _repositoryMock.Setup(repo => repo.UpdateAsync(user, default))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.UpdateUserAsync(user);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldThrowException_WhenUserIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.UpdateUserAsync(null!));
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(1, default))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(999, default))
                .ReturnsAsync(false);

            // Act
            var result = await _userService.DeleteUserAsync(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldThrowException_WhenIdIsInvalid()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.DeleteUserAsync(0));
        }
    }
}
