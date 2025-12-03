namespace StayFit.Models.Domain
{
    public class Progress
    {
        public int Id { get; set; }
        public string ? UserId { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
    }
}
