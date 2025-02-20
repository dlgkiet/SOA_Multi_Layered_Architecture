using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class MovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        // Constructor - Dependency Injection
        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // CRUD Methods
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            try
            {
                return await _movieRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log error (use logging framework if available, for now we will just throw)
                throw new ApplicationException("An error occurred while retrieving all movies.", ex);
            }
        }

        public async Task AddMovieAsync(Movie movie)
        {
            try
            {
                // Validation: Check if a movie with the same title already exists
                var existingMovies = await _movieRepository.GetAllAsync();
                if (existingMovies.Any(m => m.Title == movie.Title))
                {
                    throw new ArgumentException("A movie with the same title already exists.");
                }

                // If validation passes, add the movie
                await _movieRepository.AddAsync(movie);
            }
            catch (ArgumentException ex)
            {
                // Validation exception is already handled above
                throw new ApplicationException("Validation error occurred while adding the movie.", ex);
            }
            catch (Exception ex)
            {
                // Log error (use logging framework if available)
                throw new ApplicationException("An error occurred while adding the movie.", ex);
            }
        }

        // Stored Procedure Method with Error Handling
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            try
            {
                return await _movieRepository.GetTopRatedMoviesWithSpAsync(topCount);
            }
            catch (Exception ex)
            {
                // Log error (use logging framework if available)
                throw new ApplicationException("An error occurred while retrieving top-rated movies.", ex);
            }
        }
    }
}
