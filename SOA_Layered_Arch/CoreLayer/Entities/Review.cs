using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOA_Layered_Arch.CoreLayer.Entities
{

    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieSeriesId { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;


    }
}
