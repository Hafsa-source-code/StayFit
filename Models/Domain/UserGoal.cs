using System.ComponentModel.DataAnnotations;

namespace StayFit.Models.Domain
{
    public class UserGoal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ? UserId { get; set; }   // link to ApplicationUser

        [Required]
        public int GoalId { get; set; }      // FK to Goal table

        public Goal ? Goal { get; set; }       // navigation property
    }
}
