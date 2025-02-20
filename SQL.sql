CREATE DATABASE MOVIE

CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE MoviesSeries (
 movie_series_id INT PRIMARY KEY IDENTITY,
 title VARCHAR(100) NOT NULL,
 genre VARCHAR(50),
 release_date DATE,
 description TEXT
);CREATE TABLE Reviews (
 review_id INT PRIMARY KEY IDENTITY,
 user_id INT NOT NULL,
 movie_series_id INT NOT NULL,
 review_text TEXT,
 review_date DATETIME DEFAULT GETDATE(),
 FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
 FOREIGN KEY (movie_series_id) REFERENCES
MoviesSeries(movie_series_id) ON DELETE CASCADE
);

CREATE TABLE Ratings (
 rating_id INT PRIMARY KEY IDENTITY,
 user_id INT NOT NULL,
 movie_series_id INT NOT NULL,
 rating DECIMAL(3, 2) CHECK (rating >= 0 AND rating <= 10),
 FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
 FOREIGN KEY (movie_series_id) REFERENCES
MoviesSeries(movie_series_id) ON DELETE CASCADE
);CREATE TABLE Tags (
 tag_id INT PRIMARY KEY IDENTITY,
 tag_name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE MovieSeriesTags (
 movie_series_id INT NOT NULL,
 tag_id INT NOT NULL,
 PRIMARY KEY (movie_series_id, tag_id),
 FOREIGN KEY (movie_series_id) REFERENCES
MoviesSeries(movie_series_id) ON DELETE CASCADE,
 FOREIGN KEY (tag_id) REFERENCES Tags(tag_id) ON DELETE CASCADE
);CREATE PROCEDURE GetMovieReviews
 @movie_series_id INT
AS
BEGIN
 SELECT r.review_id, r.user_id, u.username, r.review_text,
r.review_date
 FROM Reviews r
 JOIN Users u ON r.user_id = u.user_id
 WHERE r.movie_series_id = @movie_series_id
 ORDER BY r.review_date DESC;
END;

CREATE PROCEDURE AddReview
 @user_id INT,
 @movie_series_id INT,
 @review_text TEXT
AS
BEGIN
 INSERT INTO Reviews (user_id, movie_series_id, review_text,
review_date)
 VALUES (@user_id, @movie_series_id, @review_text, GETDATE());
END;

CREATE PROCEDURE GetTopRatedMovies
 @top_count INT
AS
BEGIN
 SELECT ms.movie_series_id, ms.title, AVG(r.rating) AS avg_rating
 FROM MoviesSeries ms
 JOIN Ratings r ON ms.movie_series_id = r.movie_series_id
 GROUP BY ms.movie_series_id, ms.title
 ORDER BY avg_rating DESC
 OFFSET 0 ROWS FETCH NEXT @top_count ROWS ONLY;
END;

CREATE PROCEDURE GetMoviesByTag
 @tag_name VARCHAR(50)
AS
BEGIN
 SELECT ms.movie_series_id, ms.title, ms.genre, ms.release_date
 FROM MoviesSeries ms
 JOIN MovieSeriesTags mst ON ms.movie_series_id = mst.movie_series_id
 JOIN Tags t ON mst.tag_id = t.tag_id
 WHERE t.tag_name = @tag_name;
END;-- INSERT DỮ LIỆU MẪU TRƯỚC KHI CHÈN-- Kiểm tra lấy danh sách đánh giá của một bộ phim
EXEC GetMovieReviews @movie_series_id = 1;

-- Kiểm tra thêm một đánh giá mới
EXEC AddReview @user_id = 1, @movie_series_id = 1, @review_text = 'Amazing movie!';

-- Kiểm tra lấy danh sách các bộ phim có xếp hạng cao nhất
EXEC GetTopRatedMovies @top_count = 5;

-- Kiểm tra lọc phim theo thẻ (tag)
EXEC GetMoviesByTag @tag_name = 'Action';





