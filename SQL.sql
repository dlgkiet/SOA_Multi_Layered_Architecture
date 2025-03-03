﻿-- Xoá database nếu tồn tại và tạo lại
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

-- Tạo bảng MoviesSeries (Thêm cột Id để liên kết với Movies)
CREATE TABLE MoviesSeries (
    movie_series_id INT PRIMARY KEY IDENTITY,
    Id INT NOT NULL,  -- Cột này để liên kết với Movies
    title VARCHAR(100) NOT NULL,
    genre VARCHAR(50),
    ReleaseDate DATE,
    description TEXT,
    FOREIGN KEY (Id) REFERENCES Movies(Id) ON DELETE CASCADE
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

<<<<<<< HEAD

INSERT INTO Tags (tag_name) VALUES 
('Sci-Fi'),
('Action'),
('Drama'),
('Romance'),
('Adventure'),
('Thriller');


INSERT INTO MovieSeriesTags (movie_series_id, tag_id) VALUES
(1, 1), -- MovieSeriesId = 1, TagId = 1 (Sci-Fi)
(1, 2), -- MovieSeriesId = 1, TagId = 2 (Action)
(2, 3), -- MovieSeriesId = 2, TagId = 3 (Drama)
(2, 4), -- MovieSeriesId = 2, TagId = 4 (Romance)
(3, 5), -- MovieSeriesId = 3, TagId = 5 (Adventure)
(3, 6); -- MovieSeriesId = 3, TagId = 6 (Thriller)

=======
-- Chèn dữ liệu vào bảng MoviesSeries
INSERT INTO MoviesSeries (title, genre, ReleaseDate, description, Id) VALUES
('Inception Series', 'Sci-Fi', '2010-07-16', 'A series based on Inception movie.', 1),
('The Dark Knight Trilogy', 'Action', '2008-07-18', 'Trilogy featuring Batman and Joker.', 2),
('Interstellar Chronicles', 'Sci-Fi', '2014-11-07', 'Extended universe of Interstellar.', 3),
('Titanic Memories', 'Romance', '1997-12-19', 'Stories beyond the Titanic movie.', 4),
('Avengers Saga', 'Action', '2019-04-26', 'Full timeline of Avengers movies.', 5);

-- Chèn dữ liệu vào bảng Reviews
INSERT INTO Reviews (user_id, movie_series_id, review_text) VALUES
(1, 1, 'Mind-blowing concept and execution!'),
(2, 2, 'Heath Ledger was legendary as Joker.'),
(3, 3, 'A must-watch for Sci-Fi lovers.'),
(1, 4, 'Heartbreaking yet beautiful story.'),
(2, 5, 'The best superhero movie ever!');

-- Chèn dữ liệu vào bảng Ratings
INSERT INTO Ratings (user_id, movie_series_id, rating) VALUES
(1, 1, 9.5),
(2, 2, 5.0),
(3, 3, 9.0),
(1, 4, 8.5),
(2, 5, 9.8);

-- Chèn dữ liệu vào bảng Tags
INSERT INTO Tags (tag_name) VALUES 
('Sci-Fi'),
('Action'),
('Romance'),
('Drama'),
('Superhero');
>>>>>>> kiet-feat/api-user-tag
