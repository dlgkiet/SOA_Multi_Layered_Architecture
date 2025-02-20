using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Layered_Arch.CoreLayer.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")] // Map property Id với cột user_id
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")] // Map property Username với cột username
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Column("email")] // Map property Email với cột email
        public string Email { get; set; }

        [Column("created_at")] // Map property CreatedAt với cột created_at
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
