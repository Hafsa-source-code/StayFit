using Dapper;
using Microsoft.EntityFrameworkCore;
using StayFit.Data;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using System.Data;

namespace StayFit.Models.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public ProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Creating a db e connection
        private IDbConnection CreateConnection() => _context.Database.GetDbConnection();

        // Get all progress records for a specific user
        public IEnumerable<Progress> GetUserProgress(string userId)
        {
            using var db = CreateConnection();
            db.Open();
            return db.Query<Progress>(
                "SELECT * FROM ProgressRecords WHERE UserId=@UserId ORDER BY Date ASC",
                new { UserId = userId });
        }

        // Adding  new progress record
        public void Add(Progress progress)
        {
            using var db = CreateConnection();
            db.Open();
            var sql = @"INSERT INTO ProgressRecords (UserId, Weight, Date)
                        VALUES (@UserId, @Weight, @Date)";
            db.Execute(sql, progress);
        }
    }
}
