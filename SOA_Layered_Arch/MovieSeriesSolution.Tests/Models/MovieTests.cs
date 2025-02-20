using SOA_Layered_Arch.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieSeriesSolution.Tests.Models
{
    public class MovieTests
    {
        [Fact]
        public void Movie_ShouldRequireTitle()
        {
            // Arrange
            var movie = new Movie { Genre = "Sci-Fi" }; // Title is missing
             // Act
             var validationResults = ValidateModel(movie);
            // Assert
            Assert.Contains(validationResults, v =>
           v.MemberNames.Contains("Title"));
        }
        [Fact]
        public void Movie_ShouldRequireGenre()
        {
            // Arrange
            var movie = new Movie { Title = "Inception" }; // Genre is missing
             // Act
             var validationResults = ValidateModel(movie);
            // Assert
            Assert.Contains(validationResults, v =>
           v.MemberNames.Contains("Genre"));
        }
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null,
           null);
            Validator.TryValidateObject(model, validationContext,
           validationResults, true);
            return validationResults;
        }
    }

}
