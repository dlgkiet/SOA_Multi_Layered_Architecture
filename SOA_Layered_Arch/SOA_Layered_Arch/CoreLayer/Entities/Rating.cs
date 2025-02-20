using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        [Required]
        public int MovieId { get; set; } // Liên kết với Movie

        [Required]
        public int UserId { get; set; } // Liên kết với User

        [Required]
        [Range(0, 10)] // Giới hạn giá trị từ 0 - 10
        public decimal Value { get; set; } // Điểm đánh giá

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời gian đánh giá
        
    }
}
