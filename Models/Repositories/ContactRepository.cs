using Dapper;
using Microsoft.EntityFrameworkCore;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using StayFit.Data;

namespace StayFit.Models.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ContactMessage> GetAll()
        {
            using var db = _context.Database.GetDbConnection();
            db.Open();
            return db.Query<ContactMessage>("SELECT * FROM ContactMessages ORDER BY Date DESC");
        }

        // Add a new contact message
        public void Add(ContactMessage message)
        {
            using var db = _context.Database.GetDbConnection();
            db.Open();
            db.Execute(
                "INSERT INTO ContactMessages (Id, Email, Message, Date) VALUES (@Id, @Email, @Message, @Date)",
                new { message.Id, message.Email, message.Message, message.Date }
            );
        }
    }
}
