using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TagName { get; set; }

        // Navigation properties
        public ICollection<MovieSeriesTag> MovieSeriesTags { get; set; }
    }
}
