namespace StayFit.Models.Domain
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string ? Email { get; set; }
        public string ? Message { get; set; }
        public DateTime Date { get; set; }
    }
}
