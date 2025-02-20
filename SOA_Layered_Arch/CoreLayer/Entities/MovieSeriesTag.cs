using MovieDatabase.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("MovieSeriesTags")]
    public class MovieSeriesTag
    {
        [Key, Column(Order = 0)]
        public int MovieSeriesId { get; set; }

        [Key, Column(Order = 1)]
        public int TagId { get; set; }

        // Navigation properties
        [ForeignKey("MovieSeriesId")]
        public MovieSeries MovieSeries { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
