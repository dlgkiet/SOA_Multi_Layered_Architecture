using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using SOA_Layered_Arch.CoreLayer.Entities;

namespace MovieDatabase.Models
{
    [Table("MoviesSeries")]
    public class MovieSeries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieSeriesId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Description { get; set; }

        // Foreign Key liên kết với Movies
        public int? MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        // Navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<MovieSeriesTag> MovieSeriesTags { get; set; }
    }
}
