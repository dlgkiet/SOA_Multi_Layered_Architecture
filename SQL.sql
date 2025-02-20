-- Xoá database nếu tồn tại và tạo lại
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MOVIE')
    DROP DATABASE MOVIE;
GO

CREATE DATABASE MOVIE;
GO

USE MOVIE;

-- Tạo bảng Users
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    created_at DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Movies
CREATE TABLE Movies (
    Id INT PRIMARY KEY IDENTITY,
    title VARCHAR(100) NOT NULL,
    genre VARCHAR(50),
    ReleaseDate DATE,
    director VARCHAR(100),
    duration INT, -- Thời lượng phim (phút)
    language VARCHAR(50),
    country VARCHAR(100),
    description TEXT
);

-- Tạo bảng MoviesSeries
CREATE TABLE MoviesSeries (
    movie_series_id INT PRIMARY KEY IDENTITY,
    title VARCHAR(100) NOT NULL,
    genre VARCHAR(50),
    ReleaseDate DATE,
    description TEXT
);

-- Tạo bảng Reviews
CREATE TABLE Reviews (
    review_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL,
    movie_series_id INT NOT NULL,
    review_text TEXT,
    review_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (movie_series_id) REFERENCES MoviesSeries(movie_series_id) ON DELETE CASCADE
);

-- Tạo bảng Ratings
CREATE TABLE Ratings (
    rating_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL,
    movie_series_id INT NOT NULL,
    rating DECIMAL(3,2) CHECK (rating >= 0 AND rating <= 10),
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (movie_series_id) REFERENCES MoviesSeries(movie_series_id) ON DELETE CASCADE
);

-- Tạo bảng Tags
CREATE TABLE Tags (
    tag_id INT PRIMARY KEY IDENTITY,
    tag_name VARCHAR(50) NOT NULL UNIQUE
);

-- Tạo bảng MovieSeriesTags
CREATE TABLE MovieSeriesTags (
    movie_series_id INT NOT NULL,
    tag_id INT NOT NULL,
    PRIMARY KEY (movie_series_id, tag_id),
    FOREIGN KEY (movie_series_id) REFERENCES MoviesSeries(movie_series_id) ON DELETE CASCADE,
    FOREIGN KEY (tag_id) REFERENCES Tags(tag_id) ON DELETE CASCADE
);

-- Cập nhật MoviesSeries để liên kết với Movies
ALTER TABLE MoviesSeries
ADD Id INT FOREIGN KEY REFERENCES Movies(Id) ON DELETE CASCADE;

-- Chèn dữ liệu vào bảng Users
INSERT INTO Users (username, email) VALUES 
('john_doe', 'john@example.com'),
('jane_smith', 'jane@example.com'),
('mike_jordan', 'mike@example.com');

-- Chèn dữ liệu vào bảng Movies
INSERT INTO Movies (title, genre, ReleaseDate, director, duration, language, country, description) VALUES
('Inception', 'Sci-Fi', '2010-07-16', 'Christopher Nolan', 148, 'English', 'USA', 'A thief who enters the dreams of others.'),
('The Dark Knight', 'Action', '2008-07-18', 'Christopher Nolan', 152, 'English', 'USA', 'Batman faces Joker in Gotham City.'),
('Interstellar', 'Sci-Fi', '2014-11-07', 'Christopher Nolan', 169, 'English', 'USA', 'A journey to save humanity from extinction.'),
('Titanic', 'Romance', '1997-12-19', 'James Cameron', 195, 'English', 'USA', 'A love story aboard the Titanic ship.'),
('Avengers: Endgame', 'Action', '2019-04-26', 'Anthony Russo, Joe Russo', 181, 'English', 'USA', 'Superheroes battle to undo Thanos''s snap.');
