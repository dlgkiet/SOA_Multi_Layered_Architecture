using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } // Tên đăng nhập

        [Required]
        [MaxLength(100)]
        [EmailAddress] // Đảm bảo định dạng email hợp lệ
        public string Email { get; set; } // Địa chỉ email

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } // Mật khẩu đã mã hóa

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Ngày tạo tài khoản

        // Navigation Properties
        public virtual ICollection<Review> Reviews { get; set; } // Liên kết với Review
        public virtual ICollection<Rating> Ratings { get; set; } // Liên kết với Rating
    }
}
