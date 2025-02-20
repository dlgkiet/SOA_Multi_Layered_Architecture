using System;
using System.Collections.Generic;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    public class Movie
    {
        public int Id { get; set; }  // PRIMARY KEY

        public string Title { get; set; }  // NOT NULL

        public string? Genre { get; set; }  // Có thể NULL

        public DateTime? ReleaseDate { get; set; }  // Có thể NULL

        public string? Director { get; set; }  // Đạo diễn (có thể NULL)

        public int? Duration { get; set; }  // Thời lượng phim (có thể NULL)

        public string? Language { get; set; }  // Ngôn ngữ (có thể NULL)

        public string? Country { get; set; }  // Quốc gia (có thể NULL)

        public string? Description { get; set; }  // Mô tả phim (có thể NULL)

        // Quan hệ 1-n với MovieSeriesTag (nếu có)
        public ICollection<MovieSeriesTag>? MovieSeriesTags { get; set; }
    }
}
