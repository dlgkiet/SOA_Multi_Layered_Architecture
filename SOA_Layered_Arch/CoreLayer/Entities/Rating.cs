using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieDatabase.Models;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieSeriesId { get; set; }

        [Required]
        [Range(0, 10)]
        public decimal RatingValue { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("MovieSeriesId")]
        public MovieSeries MovieSeries { get; set; }
    }
}
