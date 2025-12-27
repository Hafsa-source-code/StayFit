using System;
using System.ComponentModel.DataAnnotations;

namespace StayFit.Models.Domain
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? CreatedByUserId { get; set; }
    }
}
