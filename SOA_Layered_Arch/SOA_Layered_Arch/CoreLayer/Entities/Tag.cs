using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        [Required]
        [MaxLength(50)] // Giới hạn độ dài tên Tag
        public string Name { get; set; } // Tên của Tag (Ví dụ: Action, Comedy)

        // Navigation Property
        public virtual ICollection<MovieSeriesTag> MovieSeriesTags { get; set; }
    }
}
