using Microsoft.AspNetCore.Mvc;
using Moq;
using SOA_Layered_Arch.APILayer.Controllers;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.ServiceLayer;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieSeriesSolution.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<UserService> _serviceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _serviceMock = new Mock<UserService>(MockBehavior.Strict);
            _controller = new UserController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "user1", Email = "user1@example.com" },
                new User { Id = 2, Username = "user2", Email = "user2@example.com" }
            };

            _serviceMock.Setup(s => s.GetAllUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetUser_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "testuser", Email = "test@example.com" };

            _serviceMock.Setup(s => s.GetUserByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetUser(1, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task GetUser_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Act
            var result = await _controller.GetUser(0, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetUserByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.GetUser(999, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddUser_ReturnsCreatedAtAction_WhenUserIsValid()
        {
            // Arrange
            var user = new User { Id = 1, Username = "newuser", Email = "newuser@example.com" };

            _serviceMock.Setup(s => s.AddUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.AddUser(user, CancellationToken.None);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetUser), createdResult.ActionName);
            Assert.Equal(user.Id, createdResult.RouteValues["id"]);
            Assert.Equal(user, createdResult.Value);
        }

        [Fact]
        public async Task AddUser_ReturnsBadRequest_WhenUserIsNull()
        {
            // Act
            var result = await _controller.AddUser(null, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User data is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOkResult_WhenUserIsUpdated()
        {
            // Arrange
            var user = new User { Id = 1, Username = "updateduser", Email = "updated@example.com" };

            _serviceMock.Setup(s => s.UpdateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.UpdateUser(1, user, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            var user = new User { Id = 2, Username = "mismatch", Email = "mismatch@example.com" };

            // Act
            var result = await _controller.UpdateUser(1, user, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid user data.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new User { Id = 999, Username = "nonexistent", Email = "nonexistent@example.com" };

            _serviceMock.Setup(s => s.UpdateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.UpdateUser(999, user, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteUserAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(1, CancellationToken.None);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Act
            var result = await _controller.DeleteUser(0, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteUserAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(999, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
