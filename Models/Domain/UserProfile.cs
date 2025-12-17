namespace StayFit.Models.Domain
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string ? UserId { get; set; }
         public string? FullName { get; set; } 
        public string ? Gender { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public string ? GoalType { get; set; }
    }
}
