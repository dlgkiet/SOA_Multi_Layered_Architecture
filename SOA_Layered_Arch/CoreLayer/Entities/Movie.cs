using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [MaxLength(100)]
        public string Director { get; set; }

        public int? Duration { get; set; } // Thời lượng phim (phút)

        [MaxLength(50)]
        public string Language { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        public string Description { get; set; }

    }
}
