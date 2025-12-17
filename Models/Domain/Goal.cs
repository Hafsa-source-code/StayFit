namespace StayFit.Models.Domain
{
    public class Goal
    {
        public int Id { get; set; }
        public string ? GoalType { get; set; } // "WeightLoss" or "WeightGain"
        public string ? Description { get; set; }
    }
}
