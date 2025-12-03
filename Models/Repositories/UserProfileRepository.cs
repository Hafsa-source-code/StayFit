using Dapper;
using Microsoft.EntityFrameworkCore;
using StayFit.Data;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using System.Data;

namespace StayFit.Models.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a database connection
        private IDbConnection CreateConnection() => _context.Database.GetDbConnection();

        // Get a user profile by UserId
        public UserProfile ? GetByUserId(string userId)
        {
            using var db = CreateConnection();
            db.Open();
            return db.QueryFirstOrDefault<UserProfile>(
                "SELECT * FROM UserProfiles WHERE UserId=@UserId",
                new { UserId = userId });
        }

        // Add a new user profile
        public void Add(UserProfile profile)
        {
            using var db = CreateConnection();
            db.Open();
            var sql = @"INSERT INTO UserProfiles (UserId, Gender, Age, Weight, Height)
                        VALUES (@UserId, @Gender, @Age, @Weight, @Height)";
            db.Execute(sql, profile);
        }

        // Update an existing user profile
        public void Update(UserProfile profile)
        {
            using var db = CreateConnection();
            db.Open();
            var sql = @"UPDATE UserProfiles 
                        SET Gender=@Gender, Age=@Age, Weight=@Weight, Height=@Height
                        WHERE UserId=@UserId";
            db.Execute(sql, profile);
        }
    }
}
