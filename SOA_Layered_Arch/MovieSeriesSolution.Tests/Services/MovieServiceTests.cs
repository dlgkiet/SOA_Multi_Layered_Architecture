using Moq;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using SOA_Layered_Arch.ServiceLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MovieSeriesSolution.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IRepository<Movie>> _repositoryMock;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Movie>>();
            _movieService = new MovieService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddMovieAsync_ShouldCallRepositoryAddAsync()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Inception",
                Genre = "Sci-Fi"
            };

            _repositoryMock.Setup(repo => repo.AddAsync(movie, default))
                .ReturnsAsync(movie); // ✅ Sửa lỗi: Trả về movie thay vì Task.CompletedTask

            // Act
            var result = await _movieService.AddMovieAsync(movie);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(movie, default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(movie.Title, result.Title);
        }

        [Fact]
        public async Task GetAllMoviesAsync_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Title = "Inception" },
                new Movie { Title = "The Matrix" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync(default))
                .ReturnsAsync(movies); // ✅ Thêm `default` vào GetAllAsync

            // Act
            var result = await _movieService.GetAllMoviesAsync();

            // Assert
            Assert.Equal(movies, result);
            Assert.Equal(2, result.Count()); // ✅ Kiểm tra số lượng phần tử trong danh sách
        }
    }
}
