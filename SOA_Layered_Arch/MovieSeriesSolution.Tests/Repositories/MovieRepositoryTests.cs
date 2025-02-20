using Moq;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieSeriesSolution.Tests.Repositories
{
    public class MovieRepositoryTests
    {
        private readonly Mock<IRepository<Movie>> _repositoryMock; // ✅ Sửa lại thành IRepository<Movie>

        public MovieRepositoryTests()
        {
            _repositoryMock = new Mock<IRepository<Movie>>();
        }

        [Fact]
        public async Task AddAsync_ShouldCallAddAsync_WhenMovieIsValid()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Inception",
                Genre = "Sci-Fi"
            };

            _repositoryMock.Setup(repo => repo.AddAsync(movie, default))
                .ReturnsAsync(movie); // ✅ Sửa từ Task.CompletedTask thành ReturnsAsync(movie)

            // Act
            var result = await _repositoryMock.Object.AddAsync(movie);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(movie, default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(movie.Title, result.Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Title = "Inception" },
                new Movie { Title = "The Matrix" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync(default))
                .ReturnsAsync(movies);

            // Act
            var result = await _repositoryMock.Object.GetAllAsync();

            // Assert
            Assert.Equal(movies, result);
            Assert.Equal(2, result.Count()); // ✅ Kiểm tra số lượng phần tử
        }
    }
}
