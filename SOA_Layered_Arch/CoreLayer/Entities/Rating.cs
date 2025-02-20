using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

    }
}
