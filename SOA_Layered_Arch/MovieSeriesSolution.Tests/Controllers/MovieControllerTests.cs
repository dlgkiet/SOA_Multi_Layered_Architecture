using Microsoft.AspNetCore.Mvc;
using Moq;
using SOA_Layered_Arch.APILayer.Controllers;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using SOA_Layered_Arch.ServiceLayer;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieSeriesSolution.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly Mock<MovieService> _serviceMock;
        private readonly MovieController _controller;

        public MovieControllerTests()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(); // ✅ Mock repository
            _serviceMock = new Mock<MovieService>(repositoryMock.Object); // ✅ Mock MovieService
            _controller = new MovieController(_serviceMock.Object); // ✅ Truyền vào Controller
        }

        [Fact]
        public async Task GetMovies_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Title = "Inception" },
                new Movie { Title = "The Matrix" }
            };

            _serviceMock.Setup(s =>
                s.GetAllMoviesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(movies);

            // Act
            var result = await _controller.GetAllMovies(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(movies, okResult.Value);
        }

        [Fact]
        public async Task AddMovie_ReturnsCreatedAtActionResult_WhenMovieIsValid()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Inception",
                Genre = "Sci-Fi"
            };

            _serviceMock.Setup(s =>
                s.AddMovieAsync(It.IsAny<Movie>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(movie);

            // Act
            var result = await _controller.AddMovie(movie, CancellationToken.None);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetMovie), createdResult.ActionName);
            Assert.Equal(movie.Id, createdResult.RouteValues["id"]);
            Assert.Equal(movie, createdResult.Value);
        }
    }
}
