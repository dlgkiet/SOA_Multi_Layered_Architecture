using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("MovieSeriesTags")]
    public class MovieSeriesTag
    {
        [Key, Column("movie_series_id", Order = 0)]
        public int MovieSeriesId { get; set; }

        [Key, Column("tag_id", Order = 1)]
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
