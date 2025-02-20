using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tag_id")] // Map property TagId với cột 'tag_id'
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("tag_name")] // Map property TagName với cột 'tag_name'
        public string TagName { get; set; }

        [JsonIgnore]
        public ICollection<MovieSeriesTag>? MovieSeriesTags { get; set; }
    }
}
