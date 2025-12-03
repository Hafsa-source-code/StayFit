using Dapper;
using Microsoft.EntityFrameworkCore;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using StayFit.Data;

namespace StayFit.Models.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all feedback
        public IEnumerable<Feedback> GetAll()
        {
            using var db = _context.Database.GetDbConnection();
            db.Open();
            return db.Query<Feedback>("SELECT * FROM Feedbacks ORDER BY Date DESC");
        }

        // Add a new feedback
        public void Add(Feedback feedback)
        {
            using var db = _context.Database.GetDbConnection();
            db.Open();
            db.Execute(
                "INSERT INTO Feedbacks (UserId, Message, Date) VALUES (@UserId, @Message, @Date)",
                new { feedback.UserId, feedback.Message, feedback.Date }
            );
        }
    }
}
