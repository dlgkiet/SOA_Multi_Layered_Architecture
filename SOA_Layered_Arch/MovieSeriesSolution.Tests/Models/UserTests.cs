using SOA_Layered_Arch.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace MovieSeriesSolution.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_ShouldRequireUsername()
        {
            // Arrange
            var user = new User { Email = "test@example.com" }; // Thiếu Username

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Username"));
        }

        [Fact]
        public void User_ShouldRequireEmail()
        {
            // Arrange
            var user = new User { Username = "testuser" }; // Thiếu Email

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void User_Email_ShouldBeValidFormat()
        {
            // Arrange
            var user = new User { Username = "testuser", Email = "invalid-email" }; // Email không hợp lệ

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void User_Username_ShouldNotExceedMaxLength()
        {
            // Arrange
            var longUsername = new string('a', 51); // 51 ký tự (vượt quá `MaxLength(50)`)
            var user = new User { Username = longUsername, Email = "test@example.com" };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Username"));
        }

        [Fact]
        public void User_Email_ShouldNotExceedMaxLength()
        {
            // Arrange
            var longEmail = new string('a', 101) + "@example.com"; // 101+ ký tự (vượt quá `MaxLength(100)`)
            var user = new User { Username = "testuser", Email = longEmail };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void User_CreatedAt_ShouldHaveDefaultValue()
        {
            // Arrange
            var user = new User { Username = "testuser", Email = "test@example.com" };

            // Act
            var createdAt = user.CreatedAt;

            // Assert
            Assert.True(createdAt <= DateTime.Now && createdAt > DateTime.Now.AddMinutes(-1)); // Kiểm tra CreatedAt không null và nằm trong khoảng hợp lý
        }

        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
