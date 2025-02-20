using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    public class MovieSeriesTag
    {
        [Key]
        public int Id { get; set; } // Khóa chính tự động

        [Required]
        public int MovieId { get; set; } // Liên kết với bảng Movie

        [Required]
        public int TagId { get; set; } // Liên kết với bảng Tag

        // Navigation Properties
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
